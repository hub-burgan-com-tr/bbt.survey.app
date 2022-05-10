using Core.Entities;
using System.Linq.Expressions;

namespace Core.DataAccess
{
    //Generic constraint uygulandı.(Yeni özellikler eklendiğinde yazılımcının
    //yazılan kurallara uygun kod yazmasını sağlarız. constrain sayesinde
    //T yi IEntity veya onu implemente eden herhangi bir referans tip olarak kısıtladık.
    //Bu sayede yazılımcı herhangi bir referans tip tanımlayamamış olacak.
    //new yazarak IEntity yazmasını engelledik. IEntity soyut olduğu için new lenemez.
    //)
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        T Get(Expression<Func<T, bool>> filter);
        List<T> GetAll(Expression<Func<T, bool>>? filter = null);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
