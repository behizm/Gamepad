using System;
using Gamepad.Service.Data.Entities;

namespace Gamepad.Service.Models.CrossModels
{
    public class ArticleSearchModel
    {
        public string Title { get; set; }

        public string Name { get; set; }

        public ArticleType? Type { get; set; }

        public GamePlatform? Platform { get; set; }

        public DateTime? ReleaseDateFrom { get; set; }

        public DateTime? ReleaseDateTo { get; set; }

        public short? SiteScoreFrom { get; set; }

        public short? SiteScoreTo { get; set; }

        public short? UserScoresAverageFrom { get; set; }

        public short? UserScoresAverageTo { get; set; }

        public Guid? GenreId { get; set; }

        public Guid? CrewId { get; set; }
    }
}
