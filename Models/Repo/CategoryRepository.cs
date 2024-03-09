
namespace GameStore.Models;

public class CategoryRepository : IRepository<Category>
{
      public IList<Category> Categories {get; set;}
    public CategoryRepository()
    {
        Categories = new List<Category>()
        {
            new Category
            {
                Id = 1,
                Name = "XBox"
            }
        };
    }

  
    

    public void Add(Category item)
    {
        throw new NotImplementedException();
    }

    public Category Find(int Id)
    {
        throw new NotImplementedException();
    }

    public IList<Category> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Remove(int Id)
    {
        throw new NotImplementedException();
    }

    public void Update(int Id, Category item)
    {
        throw new NotImplementedException();
    }
}