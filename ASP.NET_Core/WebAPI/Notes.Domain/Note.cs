namespace Notes.Domain;

/// <summary> Класс представляющий заметку </summary>
public class Note {
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Title { get; set; }
    public string? Details { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? EditDate { get; set; }
}
