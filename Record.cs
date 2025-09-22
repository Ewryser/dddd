using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEM3_PR1_MODEL
{
    public class Record
    {
        public int Id { get; set; }             // Уникальный идентификатор
        public string Name { get; set; }        // Название пластинки
        public string Musician { get; set; }    // Исполнитель
        public string Year { get; set; }        // Год выпуска
        public string Janre { get; set; }       // Жанр
        public string Laybel { get; set; }    // Клиническая картина
        public string NativeRegion { get; set; } // Страна исполнителя

        public override string ToString()
        {
            return $"{Id}. {Name} (лат.'{Musician}') — год: {Year}, жанр: {Janre}";
        }
    }
}
