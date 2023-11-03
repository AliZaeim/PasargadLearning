using PLDataLayer.Entities.Blog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PLCore.Services.Interfaces
{
    public interface IBlogService
    {
        #region General
        public void SaveChanges();
        public Task SaveChangesAsync();
        #endregion General
        #region NewsGroup
        public Task CreateNewsGroupAsync(NewsGroup newsGroup);
        public void CreateNewsGroup(NewsGroup newsGroup);
        public void UpdateNewsGroup(NewsGroup newsGroup);
        public Task<NewsGroup> GetNewsGroupByIdAsync(int id);
        public Task<List<NewsGroup>> GetNewsGroupsAsync();
        public Task RemoveNewsGroup(int id);
        public bool ExistNewsGroup(int id);
        #endregion NewsGroup
        #region Publisher
        public Task<List<Publisher>> GetPublishersAsync();
        public void CreatePublisher(Publisher publisher);
        public void UpdatePublisher(Publisher publisher);
        public void RemovePublisher(int id);
        public bool ExistsPublisher(int id);
        public Task<Publisher> GetPublisherByIdAsync(int id);
        #endregion
        #region News
        public void CreateNews(News news);
        public Task CreageNewsAsync(News news);
        public void UpdateNews(News news);
        public Task<List<News>> GetNewsAsync();
        public Task<News> GetNewsByIdAsync(int id);
        public Task<News> GetNewsByCodeAsync(string code);
        public Task RemoveNews(int id);
        public bool ExistNews(int id);
        public Task<List<News>> SearchNews(string serch);
        #endregion News
        #region NewsFile
        public void CreateNewsFile(NewsFile newsFile);
        public void UpdateNewsFile(NewsFile newsFile);
        public Task<NewsFile> GetNewsFileByIdAsync(int id);
        public Task<List<NewsFile>> GetNewsFilesAsync();
        public void RemoveNewsFile(NewsFile newsFile);
        public Task<List<NewsFile>> GetNewsFilesByTypeAsync(string type);
        #endregion NewsFile

    }
}
