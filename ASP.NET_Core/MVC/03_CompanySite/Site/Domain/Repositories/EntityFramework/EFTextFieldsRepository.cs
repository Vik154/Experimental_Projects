using Microsoft.EntityFrameworkCore;
using Site.Domain.Entities;
using Site.Domain.Repositories.Abstract;

namespace Site.Domain.Repositories.EntityFramework;

/// <summary> Реализация интерфейса ITextFieldsRepository для работы с текстовыми полями </summary>
public class EFTextFieldsRepository : ITextFieldsRepository {
    private readonly AppDbContext context;
    public EFTextFieldsRepository(AppDbContext context) {
        this.context = context;
    }

    public IQueryable<TextField> GetTextFields() {
        return context.TextFields;
    }

    public TextField? GetTextFieldById(Guid id) {
        return context.TextFields.FirstOrDefault(x => x.Id == id);
    }

    public TextField? GetTextFieldByCodeWord(string codeWord) {
        return context.TextFields.FirstOrDefault(x => x.CodeWord == codeWord);
    }

    public void SaveTextField(TextField entity) {
        if (entity.Id == default)
            context.Entry(entity).State = EntityState.Added;
        else
            context.Entry(entity).State = EntityState.Modified;
        context.SaveChanges();
    }

    public void DeleteTextField(Guid id) {
        context.TextFields.Remove(new TextField() { Id = id });
        context.SaveChanges();
    }
}