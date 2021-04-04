using System;
using Domain.ValueObjects;
using Domain.ValueObjects.Link;

namespace Domain {
    public class Link {
        public int Id;
        public Ref<User> User;
        public Title Title;
        public Subtitle Subtitle;
        public IconName IconName; 
        public Href Href;
        public DateTime CreatedAt;
    }
}