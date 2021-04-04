using Metadata.Exceptions;

namespace Services.Render {
    public interface IRenderService {
        
        /// <exception cref="NoUserFoundException">thrown if no user found</exception>
        public void BuildUserPage(int userId);
    }
}