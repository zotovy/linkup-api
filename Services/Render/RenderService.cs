using System.IO;
using System.Linq;
using Database;
using Domain.ValueObjects.User;
using Metadata.Exceptions;

namespace Services.Render {
    public class RenderService : IRenderService {

        private readonly string _templatesPath = Directory.GetCurrentDirectory() + "/static/templates";
        private readonly string _pagesPath = Directory.GetCurrentDirectory() + "/static/pages";
        private readonly string _linkTemplatePath = Directory.GetCurrentDirectory() + "/static/templates/link.html";
        private string _getTemplatePath(Theme theme) => _templatesPath + $"/{theme}.ejs";
        private string _getPagePath(int id) => _pagesPath + $"/{id}.html";
        private readonly string _linkHtml;

        private readonly DatabaseContext _context;

        public RenderService(DatabaseContext context) {
            _context = context;

            using var reader = new StreamReader(_linkTemplatePath);
            _linkHtml = reader.ReadToEnd();
        }

        private void Render(int userId, Theme theme, Domain.User data) {
            // get html as string
            using var reader = new StreamReader(_getTemplatePath(theme));
            var html = reader.ReadToEnd();

            // render
            foreach (var variable in data.GetType().GetProperties()) {
                html = html.Replace(
                    "{{ " + variable.Name + " }}",
                    variable.GetValue(data, null) as string ?? string.Empty
                );
            }
            
            // render links
            var renderedLinks = "";
            foreach (var linkRef in data.Links) {
                Domain.Link link = linkRef.Model;
                if (link == null) continue;

                var currLink = _linkHtml;
                currLink = currLink.Replace("{{ icon-path }}", link.IconName.Value);
                currLink = currLink.Replace("{{ title }}", link.Title.Value);
                currLink = currLink.Replace("{{ subtitle }}", link.Subtitle.Value);
                currLink = currLink.Replace("{{ href }}", link.Href.Value);

                renderedLinks += currLink;
            }

            html = html.Replace("{% links %}", renderedLinks);
            
            // save new page
            File.WriteAllText(_getPagePath(userId), html);
        }

        public void BuildUserPage(int userId) {
            // get user
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);
            if (user == null) throw new NoUserFoundException(userId);
            
            
        }
    }
}