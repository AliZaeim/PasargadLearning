using PLDataLayer.Entities.SubEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PLCore.Services.Interfaces
{
    public interface ISubEntityService
    {
        #region County
        public Task<List<County>> GetCounties();
        public Task<List<County>> GetCountiesofState(int SId);
        #endregion County
        #region State
        public Task<List<State>> GetStates();

        #endregion State
        #region About
        public Task CreateAboutAsync(About about);
        public void UpdateAbout(About about);
        public Task<About> GetAboutByIdAsync(int id);
        public Task<bool> RemoveAboutByIdAsync(int id);
        public Task<List<About>> GetAboutsAsync();
        #endregion About
        #region Slider
        public Task CreateSliderAsync(Slider slider);
        public void CreateSlider(Slider slider);
        public void UpdateSlider(Slider slider);
        public Task<Slider> GetSliderByIdAsync(int id);
        public void RemoveSlider(int id);
        public Task<List<Slider>> GetSlidersAsync();
        #endregion Slider
        #region TimeLine
        public Task CreateTimeLineAsync(Timeline timeline);
        public void CreateTimeLine(Timeline timeline);
        public void UpdateTimeLine(Timeline timeline);
        public Task<Timeline> GetTiemLineByIdAsync(int id);
        public Task RemoveTimeLineByIdAsync(int id);
        public Task<List<Timeline>> GetTimelinesAsync();
        public bool TimeLineExist(int id);
        #endregion TimeLine
        #region General
        public Task SaveChangesAsync();
        public void SaveChanges();
        #endregion
        #region TimeLineComponent
        public Task CreateTimeLineComponentAsync(TimelineComponent timelineComponent);
        public void CreateTimeLineComponent(TimelineComponent timelineComponent);
        public void UpdateTimeLineComponent(TimelineComponent timelineComponent);
        public Task<TimelineComponent> GetTimelineComponentByIdAsync(int id);
        public Task RemoveTimelineComponentAsync(int id);
        public Task<List<TimelineComponent>> GetTimelineComponentsAsync();
        public Task<List<TimelineComponent>> GetTimelineComponentsOfTimeLineAsync(int tlId);
        public bool ExistTLC(int id);
        #endregion
        #region ContactMessage
        public void CreateMessage(ContactMessage contactMessage);
        public Task<ContactMessage> GetContactByIdAsync(int id);
        public void UpdateMessage(ContactMessage contactMessage);
        public Task<List<ContactMessage>> GetContactMessagesAsync();
        #endregion
        #region ContactInfo
        public void CreateContactInfo(ContactInfo contactInfo);
        public void UpdateContactInfo(ContactInfo contactInfo);
        public Task<ContactInfo> GetLastContactInfo();
        public Task<ContactInfo> GetContactInfoById(int id);
        public Task<List<ContactInfo>> GetContactInfosAsync();
        public bool ExistContactInfo(int id);
        public void RemoveContactInfo(ContactInfo contactInfo);
        #endregion
        #region Header
        public void CreateHeader(Header header);
        public void UpdateHeader(Header header);
        public Task<Header> GetHeaderByIdAsync(int id);
        public Task<List<Header>> GetHeadersAsync();
        public Task<Header> GetLastHeader();
        public void RemoveHeader(int id);
        public bool ExistHeader(int id);
        #endregion
        #region Insta
        void Create_Insta(InstaPost instaPost);
        void Edit_Insta(InstaPost instaPost);
        Task<List<InstaPost>> GetInstaPosts();
        Task<InstaPost> GetInstaPostWithId(int id);
        public void RemoveInstaPost(int id);
        #endregion
        #region SiteFAQ
        public void CreateSiteFAQ(SiteFAQ siteFAQ);
        public void UpdateSiteFAQ(SiteFAQ siteFAQ);
        public Task RemoveSiteFAQ(int id);
        public Task<List<SiteFAQ>> GetSiteFAQs();
        public Task<SiteFAQ> GetSiteFAQById(int id);
        public bool ExistSiteFAQ(int id);
        public Task<List<SiteFAQ>> GetSiteFAQsWithoutRes();

        #endregion
        #region PageInfo
        public void CreatePageInfo(PageInfo pageInfo);
        public void UpdatePageInfo(PageInfo pageInfo);
        public Task RemovePageInfo(int id);
        public Task<List<PageInfo>> GetPageInfos();
        public Task<PageInfo> GetPageInfoById(int id);
        public bool ExistPageInfo(int id);
        public Task<PageInfo> GetPageInfoByNameAsync(string name);
        
        #endregion
        #region PackInfo
        public void CreatePackInfo(PackInfo packInfo);
        public void UpdatePackInfo(PackInfo packInfo);
        public Task RemovePackInfo(int id);
        public Task<List<PackInfo>> GetPackInfos();
        public Task<PackInfo> GetPackInfoById(int id);
        public bool ExistPackInfo(int id);
        public Task<PackInfo> GetPackInfoByName(string name);
        #endregion
        #region Separator
        public void CreateSeparator(Separator separator);
        public void UpdateSeparator(Separator Separator);
        public Task RemoveSeparator(int id);
        public Task<List<Separator>> GetSeparators();
        public Task<Separator> GetSeparatorById(int id);
        public bool ExistSeparator(int id);
        public Task<Separator> GetSeparatorByName(string name);

        #endregion
        #region EmailBank
        public void CreateEmailBank(EmailBank emailBank);
        public Task RemoveEmailBank(int id);
        public Task<List<EmailBank>> GetEmailBanks();
        public Task<EmailBank> GetEmailBankByIdAsync(int id);
        public bool ExistEmailBank(int id);
        public Task<bool> ExistEmail(string email);

        #endregion


    }
}

