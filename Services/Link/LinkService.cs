using System;
using System.Linq;
using Database;
using Database.Link;

namespace Services.Link {
    public class LinkService: ILinkService {
        private readonly DatabaseContext _context;

        public LinkService(DatabaseContext context) {
            _context = context;
        }

        /// <exception cref="ArgumentException">thrown if no author found</exception>
        public void Add(Domain.Link link) {
            var model = new LinkModel(link);
            var author = _context.Users.FirstOrDefault(x => x.Id == link.User.Id);

            // check author
            if (author == null) throw new ArgumentException($"no user found with id {link.User.Id}");
            
            // save link
            _context.Links.Add(model);

            // save because we need to generate id
            _context.SaveChanges();

            // save link to link (xd)
            author.LinkIds.Add(model.Id);

            _context.SaveChanges();
            // todo: trigger link page rebuild
        }
    }
}