using System.IO;
using System.Linq;
using Database;
using Domain.ValueObjects;
using Domain.ValueObjects.User;
using Metadata.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Services.Render {
    public class RenderService : IRenderService {

        private readonly string _templatesPath = Directory.GetCurrentDirectory() + "/static/templates";
        private readonly string _pagesPath = Directory.GetCurrentDirectory() + "/static/pages";
        private readonly string _linkTemplatePath = Directory.GetCurrentDirectory() + "/static/templates/link.html";
        private string _getTemplatePath(Theme theme) => _templatesPath + $"/{theme}.html";
        private string _getPagePath(string username) => _pagesPath + $"/{username}.html";
        private readonly string _linkHtml;

        private readonly DatabaseContext _context;
        private readonly IConfiguration _configuration;

        public RenderService(DatabaseContext context, IConfiguration configuration) {
            _context = context;
            _configuration = configuration;

            using var reader = new StreamReader(_linkTemplatePath);
            _linkHtml = reader.ReadToEnd();
        }

        private void Render(int userId, Theme theme, Domain.User data) {
            // get html as string
            using var reader = new StreamReader(_getTemplatePath(theme));
            var html = reader.ReadToEnd();

            // render
            // foreach (var variable in data.GetType().GetProperties()) {
            //     html = html.Replace(
            //         "{{ " + variable.Name + " }}",
            //         variable.GetValue(data, null) as string ?? string.Empty
            //     );
            // }

            html = html.Replace("{{ name }}", data.Name.Value);
            html = html.Replace("{{ uid }}", data.Id.ToString());
            html = html.Replace("{{ imagePath }}", data.ProfileImagePath?.Value);
            
            // render links
            var renderedLinks = "";
            foreach (var linkRef in data.Links) {
                Domain.Link link = linkRef.Model;
                if (link == null) continue;

                var currLink = _linkHtml;
                currLink = currLink.Replace("{{ iconName }}", link.IconName.Value);
                currLink = currLink.Replace("{{ title }}", link.Title.Value);
                currLink = currLink.Replace("{{ subtitle }}", link.Subtitle.Value);
                currLink = currLink.Replace("{{ href }}", link.Href.Value);

                renderedLinks += currLink;
            }

            html = html.Replace("{% links %}", renderedLinks);
            
            // save new page
            File.WriteAllText(_getPagePath(data.Username.Value), html);
        }

        public void BuildUserPage(int userId) {
            // get user
            var model = _context.Users
                .Include(x => x.Links)
                .FirstOrDefault(x => x.Id == userId);
            if (model == null) throw new NoUserFoundException(userId);
            
            var user = model.ToDomain();
            if (user.ProfileImagePath?.Value == null)
                user.ProfileImagePath = new ImagePath($"{_configuration["Server"]}/static/profile-image/default.png");
            
            Render(user.Id, user.Theme, user);
        }
    }
}