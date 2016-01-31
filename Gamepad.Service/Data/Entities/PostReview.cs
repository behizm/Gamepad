using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamepad.Service.Data.Entities
{
    public class PostReview : BaseEntity
    {
        [Required, StringLength(100)]
        public string Title { get; set; }

        private short _max;
        public short Max {
            get { return _max; }
            set
            {
                _max = value;
                if (_max > 0 && _score > 0)
                {
                    NormalScore = Math.Round(value: _score / (double)_max, digits: 2);
                }
                else
                {
                    NormalScore = 0;
                }
            }
        }

        private short _score;
        public short Score {
            get { return _score; }
            set
            {
                _score = value;
                if (_max > 0 && _score > 0)
                {
                    NormalScore = Math.Round(value: _score / (double)_max, digits: 2);
                }
                else
                {
                    NormalScore = 0;
                }
            }
        }

        [Index("IX_NormalScore")]
        public double NormalScore { get; private set; }

        public Guid PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
