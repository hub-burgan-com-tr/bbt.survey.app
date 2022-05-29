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
        //Bu expressionda T tipinde tek bir data dönecek.
        //Delege T tipinde parametre alacak ve geriye bool tipinde değer döndürecek.
        //filterla birlikte filtre verilmesini zorunlu kıldım. Çünkü tek bir data dönecek ve dönecek datayı filterla bulacak.
        T Get(Expression<Func<T, bool>> filter);
        //Yukarıda yazılan yorum satırı aynı şekilde geçerli olup tek farkı filterın null olmasıdır.
        //Filter null olduğundan istersek bütün bir datayı döneriz ya da filtre vererek yine o filtreye uyan bütün dataları getirebiliriz.
        List<T> GetAll(Expression<Func<T, bool>>? filter = null);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
