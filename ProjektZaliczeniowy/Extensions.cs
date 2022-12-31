using ProjektZaliczeniowy.Dtos;
using ProjektZaliczeniowy.Entities;

namespace Catalog
{
    public static class Extensions
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto
            {
                Id = item.Id, Name = item.Name, Price = item.Price, CreatedDate = item.CreatedDate
            };
        }
        public static ScoreDto AsDto(this Score score)
        {
            return new ScoreDto
            {
                Id = score.Id , TeacherName = score.TeacherName, Value = score.Value, CreatedDate = score.CreatedDate
            };
        }
    }
    
}