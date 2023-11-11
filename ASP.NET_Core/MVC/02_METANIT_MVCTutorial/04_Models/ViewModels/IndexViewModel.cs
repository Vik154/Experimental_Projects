using _04_Models.Models;

namespace _04_Models.ViewModels;

public class IndexViewModel {
    public IEnumerable<Person> People { get; set; } = new List<Person>();
    public IEnumerable<CompanyModel> Companies { get; set; } = new List<CompanyModel>();
}
