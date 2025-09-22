using SEM3_PR1_MODEL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PR1_SEM3_LOGIC
{
    //Реализация CRUD операций: create, read, upgrade, delete.
    public class PRLibraryLogic
    {
        private List<Record> records = new List<Record>();

        //CREATE -> добавить новую пластику.
        public void AddRecord(int id, string name, string musician, string year, string janre, string layble, string nativeRegion)
        {
            var record = new Record
            {
                Id = id,
                Name = name,
                Musician = musician,
                Year = year,
                Janre = janre,
                Laybel = layble,
                NativeRegion = nativeRegion
            };
            records.Add(record);
        }

        //Проверка Id на уникальность.
        public bool ExistsId(int id, int? excludeId = null)
        {
            return records.Any(p => p.Id == id && p.Id != excludeId);
        }


        //READ -> получить список пластинок или пластинку по Id.
        public List<Record> GetAllRecords() => records;
        public Record GetRecordById(int id) => records.FirstOrDefault(p => p.Id == id); //FOD - LINQ-запрос, возвр. 1-ый эл-т по=>ти/знач-ие по умолч.

        //UPGRADE -> изменить характеристики растения.
        public bool UpdateRecord(int id, string name, string musician, string year, string janre, string layble, string nativeRegion)
        {
            var record = GetRecordById(id);
            if (record == null) return false;

            record.Name = name;
            record.Musician = musician;
            record.Year = year;
            record.Janre = janre;
            record.Laybel = layble;
            record.NativeRegion = nativeRegion;
            return true;
        }

        //DELETE -> удалить пластинку.
        public bool DeleteRecord(int id)
        {
            var record = GetRecordById(id);
            if (record == null) return false;
            records.Remove(record);
            return true;
        }

        //Бизнес-функция 1: Группировка по жанрам.
        public Dictionary<string, List<Record>> GroupByJanre()
        {
            return records.GroupBy(p => p.Janre).ToDictionary(g => g.Key, g => g.ToList());
        }

        //Бизнес-функция 2: Поиск пластинки по году выпуска.
        public List<Record> FindByYear(string year)
        {
            return records.Where(p => p.Year.ToLower().Contains(year.ToLower())).ToList();
        }
    }
}
// 30. public PoisonousPlant GetPlantById(int id) => plants.FirstOrDefault(x => x.Id == id) - этот метод ищет пластинку по его Id.
//FirstOrDefault(...) — это метод LINQ: 1)проходит по списку plants. 2)ищет первый элемент, у которого b.Id == id.
//3)если такой элемент найден → возвращает его. 4)если нет → возвращает null (так как используется OrDefault).

// 63. plants.GroupBy(b => b.Genre) — LINQ: проходит по коллекции plants и группирует элементы по ключу b.Family.
//Возвращает IEnumerable<IGrouping<string, PoisonousPlant>>. Каждый IGrouping содержит Key (класс опасности) и перечисление растений этой группы.
//.ToDictionary(g => g.Key, g => g.ToList()) — Преобразует набор групп в Dictionary<string, List<PoisonousPlant>>, где:
//ключ — название класса опасности (g.Key), значение — список растений в этой группе (g.ToList()).
//Результат: словарь, где для каждого класса опасности — список растений.

// 69. b.Using.ToLower().Contains(use.ToLower()) — делает поиск подстроки: если в Using содержится подстрока use (без учёта регистра благодаря ToLower()), то книга подходит.
//.Where(...).ToList() — фильтрует и возвращает материализованный List<PoisonousPlants>.
//Результат: список результат, у которых в поле Toxin встречается заданная подстрока.