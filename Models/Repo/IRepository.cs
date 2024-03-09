namespace GameStore.Models;

public interface IRepository<T>{
IList<T> GetAll();
T Find(int Id);

void Add(T item);

void Update(int Id,T item);

void Remove (int Id);


}