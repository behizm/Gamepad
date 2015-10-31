using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamepad.Service.Interfaces;

namespace Gamepad.Service.Repositories
{
    internal  class ArticleService : BaseService, IArticleService
    {
        public IArticleService Shadow()
        {
            return new ArticleService();
        }
    }
}
