﻿using Site.Domain.Entities;

namespace Site.Domain.Repositories.Abstract;

public interface ITextFieldsRepository {
    
    IQueryable<TextField> GetTextFields();
    TextField? GetTextFieldById(Guid id);
    TextField? GetTextFieldByCodeWord(string codeWord);
    void SaveTextField(TextField entity);
    void DeleteTextField(Guid id);
}
