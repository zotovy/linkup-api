using System;

namespace Services.Link {
    public interface ILinkService {
        
        /// <exception cref="ArgumentException">thrown if no author found</exception>
        public int Add(Domain.Link link);
        
        /// <exception cref="ArgumentException">thrown if no link found</exception>
        public void ChangeTo(Domain.Link link);
        
        public int? GetAuthorIdOf(int id);
        
        /// <exception cref="ArgumentException">thrown if no link found</exception>
        public void Remove(int id);
    }
}