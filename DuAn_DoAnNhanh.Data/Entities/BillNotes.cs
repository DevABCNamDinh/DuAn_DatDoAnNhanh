using DuAn_DoAnNhanh.Data.Enum;

namespace DuAn_DoAnNhanh.Data.Entities
{
    public class BillNotes
    {
        public Guid BillNotesID { get; set; }
        public Guid BillID { get; set; }
        public NoteType NoteType { get; set; }
        public string NoteContent { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public virtual Bill Bill { get; set; } = null!;
    }
}
