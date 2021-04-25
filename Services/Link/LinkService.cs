using System;
using System.Data.Entity;
using System.Linq;
using Database;
using Database.Link;
using Metadata.Exceptions;
using Services.Render;

namespace Services.Link {
    public class LinkService : ILinkService {
        private readonly DatabaseContext _context;
        private readonly IRenderService _renderService;

        public LinkService(DatabaseContext context, IRenderService renderService) {
            _context = context;
            _renderService = renderService;
        }

        /// <exception cref="ArgumentException">thrown if no author found</exception>
        public int Add(Domain.Link link) {
            var model = new LinkModel(link);

            var author = _context.Users.FirstOrDefault(x => x.Id == link.User.Id);
            if (author == null) throw new NoUserFoundException();

            // save link
            _context.Links.Add(model);

            // save because we need to generate id
            _context.SaveChanges();

            // save link to link (xd)
            author.LinkIds.Add(model.Id);

            _context.SaveChanges();
            _renderService.BuildUserPage(model.UserId);

            return model.Id;
        }

        public void ChangeTo(Domain.Link link) {
            var model = _context.Links.FirstOrDefault(x => x.Id == link.Id);
            if (model == null) throw new ArgumentException($"no link found with id {link.Id}");

            if (link?.Href.Value != null) model.Href = link.Href.Value;
            if (link?.Title.Value != null) model.Title = link.Title.Value;
            if (link?.Subtitle.Value != null) model.Subtitle = link.Subtitle.Value;
            if (link?.IconName.Value != null) model.IconName = link.IconName.Value;

            _context.SaveChanges();
            
            _renderService.BuildUserPage(model.UserId);
        }

        public int? GetAuthorIdOf(int id) {
            return _context.Links.FirstOrDefault(x => x.Id == id)?.UserId;
        }

        public void Remove(int id) {
            var model = _context.Links.FirstOrDefault(x => x.Id == id);
            if (model == null) throw new ArgumentException($"no link found with id {id}");
            
            // remove link
            _context.Links.Remove(model);
            
            // remove id of this link from author
            var author = _context.Users.FirstOrDefault(x => x.Id == model.UserId);
            if (author == null) throw new NoUserFoundException();
            author.LinkIds = author.LinkIds.Where(x => x != id).ToList();
            
            _context.SaveChanges();
            _renderService.BuildUserPage(model.UserId);
        }
    }
}