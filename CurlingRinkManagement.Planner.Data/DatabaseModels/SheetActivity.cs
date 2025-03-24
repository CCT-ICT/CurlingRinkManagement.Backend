using CurlingRinkManagement.Planner.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurlingRinkManagement.Planner.Domain.DatabaseModels
{
    public class SheetActivity : IDatabaseEntity
    {
        public Guid Id { get; set; }

        //references
        [ForeignKey("Sheet")]
        public Guid SheetId { get; set; }
        public Sheet? Sheet { get; set; }

        [ForeignKey("Activity")]
        public Guid ActivityId { get; set; }
        public Activity? Activity { get; set; }
    }
}