using Microsoft.EntityFrameworkCore;

namespace ProjetoSara.Utils
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

        public PagedResult()
        {
        }

        public PagedResult(List<T> items, int totalItems, int pageNumber, int pageSize)
        {
            Items = items;
            TotalItems = totalItems;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public static async Task<PagedResult<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var totalItems = await source.CountAsync();
            var items = await source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<T>(items, totalItems, pageNumber, pageSize);
        }
    }
}