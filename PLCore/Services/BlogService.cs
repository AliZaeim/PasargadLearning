using Microsoft.EntityFrameworkCore;
using PLCore.Services.Interfaces;
using PLDataLayer.Context;
using PLDataLayer.Entities.Blog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLCore.Services
{
    public class BlogService : IBlogService
    {
        private readonly PLContext _PLContext;
        public BlogService(PLContext PLContext)
        {
            _PLContext = PLContext;
        }
        #region General
        public void SaveChanges()
        {
            _PLContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _PLContext.SaveChangesAsync();
        }
        #endregion
        #region NewsGroup
        public void CreateNewsGroup(NewsGroup newsGroup)
        {
            _PLContext.NewsGroups.Add(newsGroup);
        }

        public async Task CreateNewsGroupAsync(NewsGroup newsGroup)
        {
            await _PLContext.NewsGroups.AddAsync(newsGroup);
        }

        public async Task<NewsGroup> GetNewsGroupByIdAsync(int id)
        {
            return await _PLContext.NewsGroups.Include(r => r.News)
                .SingleOrDefaultAsync(s => s.NewsGroup_Id == id);
        }

        public async Task<List<NewsGroup>> GetNewsGroupsAsync()
        {
            return await _PLContext.NewsGroups.Include(r => r.News).ToListAsync();
        }

        public async Task RemoveNewsGroup(int id)
        {
            NewsGroup newsGroup = await _PLContext.NewsGroups.FindAsync(id);
            newsGroup.IsDeleted = true;
            _PLContext.NewsGroups.Update(newsGroup);
        }

        public void UpdateNewsGroup(NewsGroup newsGroup)
        {
            _PLContext.NewsGroups.Update(newsGroup);
        }
        public bool ExistNewsGroup(int id)
        {
            return _PLContext.NewsGroups.Any(a => a.NewsGroup_Id == id);
        }

        #endregion NewsGroup
        #region Publisher
        public async Task<List<Publisher>> GetPublishersAsync()
        {
            return await _PLContext.Publishers.Include(r => r.News).ToListAsync();
        }
        public void CreatePublisher(Publisher publisher)
        {
            _PLContext.Publishers.Add(publisher);
        }

        public void UpdatePublisher(Publisher publisher)
        {
            _PLContext.Publishers.Update(publisher);
        }

        public void RemovePublisher(int id)
        {
            Publisher publisher = _PLContext.Publishers.Find(id);
            _PLContext.Publishers.Remove(publisher);
        }

        public async Task<Publisher> GetPublisherByIdAsync(int id)
        {
            return await _PLContext.Publishers.Include(r => r.News).SingleOrDefaultAsync(s => s.Publisher_Id == id);
        }
        public bool ExistsPublisher(int id)
        {
            return _PLContext.Publishers.Any(a => a.Publisher_Id == id);
        }
        #endregion
        #region News
        public void CreateNews(News news)
        {
            _PLContext.News.Add(news);
        }

        public async Task CreageNewsAsync(News news)
        {
            await _PLContext.News.AddAsync(news);
        }

        public void UpdateNews(News news)
        {
            _PLContext.News.Update(news);
        }

        public async Task<News> GetNewsByIdAsync(int id)
        {
            return await _PLContext.News.Include(r => r.NewsGroup).Include(r => r.NewsFiles)
                .SingleOrDefaultAsync(s => s.News_Id == id);

        }
        public async Task<List<News>> GetNewsAsync()
        {
            return await _PLContext.News.Include(r => r.NewsGroup).Include(r => r.NewsFiles).Include(r => r.Publisher).ToListAsync();
        }
        public async Task RemoveNews(int id)
        {
            News news = await _PLContext.News.FindAsync(id);
            news.IsDeleted = true;
            _PLContext.News.Update(news);
        }

        public bool ExistNews(int id)
        {
            return _PLContext.News.Any(a => a.News_Id == id);
        }
        public async Task<News> GetNewsByCodeAsync(string code)
        {
            return await _PLContext.News.Include(r => r.NewsGroup).Include(r => r.NewsFiles)
                .SingleOrDefaultAsync(s => s.News_Code.Trim() == code);
        }
        public async Task<List<News>> SearchNews(string serch)
        {
            string srch = "%" + serch + "%";
            List<News> news = await _PLContext.News.Include(r => r.NewsFiles).Include(r => r.NewsGroup).Include(r => r.Publisher).ToListAsync();
            news = news.Where(w => w.News_Text.Contains(serch) || w.NewsGroup.NewsGroup_Title.Contains(serch) || w.News_Title.Contains(serch)
            || w.News_Abstract.Contains(serch) || w.Publisher.Publisher_Title.Contains(serch) || w.NewsGroup.NewsGroup_Title.Contains(serch)
            || w.News_Tags.Contains(serch))
                .ToList();
            return news;
        }
        #endregion News
        #region NewsFile
        public void CreateNewsFile(NewsFile newsFile)
        {
            _PLContext.NewsFiles.Add(newsFile);
        }

        public void UpdateNewsFile(NewsFile newsFile)
        {
            _PLContext.Update(newsFile);
        }

        public async Task<NewsFile> GetNewsFileByIdAsync(int id)
        {
            return await _PLContext.NewsFiles.Include(r => r.News).SingleOrDefaultAsync(s => s.NF_Id == id);
        }

        public async Task<List<NewsFile>> GetNewsFilesAsync()
        {
            return await _PLContext.NewsFiles.Include(r => r.News).ToListAsync();
        }

        public void RemoveNewsFile(NewsFile newsFile)
        {
            _PLContext.NewsFiles.Update(newsFile);
        }

        public async Task<List<NewsFile>> GetNewsFilesByTypeAsync(string type)
        {
            return await _PLContext.NewsFiles.Include(r => r.News).Include(r => r.News.NewsGroup)
                .Where(w => w.NF_Type.Contains(type)).ToListAsync();
        }


        #endregion NewsFile
    }
}
