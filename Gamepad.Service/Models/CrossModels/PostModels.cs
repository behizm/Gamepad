using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Gamepad.Service.Data.Entities;

namespace Gamepad.Service.Models.CrossModels
{
    public class PostUpdateModel
    {
        public Guid? Id { get; set; }

        private string _title;
        [Required, StringLength(200, MinimumLength = 4)]
        public string Title
        {
            get { return _title; }
            set
            {
                var title = value.Trim();
                while (title.Contains("  "))
                {
                    title = title.Replace("  ", " ");
                }
                Name = title.ToLower().Replace(" ", "_");
                _title = title;

            }
        }
        
        public string Name { get; private set; }

        [Required]
        public PostType? PostType { get; set; }

        [Required, StringLength(300, MinimumLength = 4)]
        public string Description { get; set; }

        public DateTime? PublishDate { get; set; }

        public string Content { get; set; }

        [Required]
        public Guid? AuthorId { get; set; }

        public Guid? MainImageId { get; set; }

        public Guid? VideoId { get; set; }

        public ICollection<string> PostTags { get; set; }

        public ICollection<Guid> Articles { get; set; }

        public ICollection<Guid> Images { get; set; }

        public ICollection<Guid> Attachments { get; set; }
    }
}
