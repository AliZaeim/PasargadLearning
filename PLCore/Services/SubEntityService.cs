using Microsoft.EntityFrameworkCore;
using PLCore.Services.Interfaces;
using PLDataLayer.Context;
using PLDataLayer.Entities.SubEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCore.Services
{
    public class SubEntityService : ISubEntityService
    {
        private readonly PLContext _PLContext;
        public SubEntityService(PLContext PLContext)
        {
            _PLContext = PLContext;
        }

        #region About
        public async Task CreateAboutAsync(About about)
        {
            await _PLContext.Aboutes.AddAsync(about);
        }
        public void UpdateAbout(About about)
        {
            _PLContext.Aboutes.Update(about);
        }
        public async Task<About> GetAboutByIdAsync(int id)
        {
            About about = await _PLContext.Aboutes.FindAsync(id);
            return about;
        }

        public async Task<bool> RemoveAboutByIdAsync(int id)
        {
            About about = await _PLContext.Aboutes.FindAsync(id);
            if (about != null)
            {
                _PLContext.Aboutes.Remove(about);
                return true;
            }
            return false;
        }

        public async Task<List<About>> GetAboutsAsync()
        {
            return await _PLContext.Aboutes.ToListAsync();
        }
        #endregion About
        #region county
        public async Task<List<County>> GetCounties()
        {
            return await _PLContext.Counties.ToListAsync();
        }
        public async Task<List<County>> GetCountiesofState(int SId)
        {
            State state = await _PLContext.States.Include(r => r.Counties).SingleOrDefaultAsync(w => w.StateId == SId);
            if (state == null)
            {
                state = await _PLContext.States.Include(r => r.Counties).SingleOrDefaultAsync(w => w.StateId == 1);
            }
            return state.Counties.ToList();
        }
        #endregion county

        #region state
        public async Task<List<State>> GetStates()
        {
            return await _PLContext.States.ToListAsync();
        }

        #endregion state
        #region Slider
        public async Task CreateSliderAsync(Slider slider)
        {
            await _PLContext.Sliders.AddAsync(slider);

        }

        public void CreateSlider(Slider slider)
        {
            _PLContext.Sliders.Add(slider);
        }

        public void UpdateSlider(Slider slider)
        {
            _PLContext.Sliders.Update(slider);
        }

        public async Task<Slider> GetSliderByIdAsync(int id)
        {
            return await _PLContext.Sliders.FindAsync(id);
        }

        public void RemoveSlider(int id)
        {
            Slider slider = _PLContext.Sliders.Find(id);
            _PLContext.Sliders.Remove(slider);
        }

        public async Task<List<Slider>> GetSlidersAsync()
        {
            return await _PLContext.Sliders.ToListAsync();
        }
        #endregion Slider
        #region TimeLine
        public async Task CreateTimeLineAsync(Timeline timeline)
        {
            await _PLContext.Timelines.AddAsync(timeline);
        }

        public void CreateTimeLine(Timeline timeline)
        {
            _PLContext.Timelines.Add(timeline);
        }

        public void UpdateTimeLine(Timeline timeline)
        {
            _PLContext.Timelines.Update(timeline);
        }

        public async Task<Timeline> GetTiemLineByIdAsync(int id)
        {
            return await _PLContext.Timelines.Include(r => r.TimelineComponents).SingleOrDefaultAsync(s => s.TL_Id == id);
        }

        public async Task RemoveTimeLineByIdAsync(int id)
        {
            Timeline timeline = await _PLContext.Timelines.FindAsync(id);
            timeline.IsDeleted = true;
            _PLContext.Timelines.Update(timeline);
        }

        public async Task<List<Timeline>> GetTimelinesAsync()
        {
            return await _PLContext.Timelines.Include(r => r.TimelineComponents)
                .ToListAsync();
        }
        public bool TimeLineExist(int id)
        {
            return _PLContext.Timelines.Any(a => a.TL_Id == id);
        }
        #endregion TimeLine
        #region TimeLineComponent
        public async Task CreateTimeLineComponentAsync(TimelineComponent timelineComponent)
        {
            await _PLContext.TimelineComponents.AddAsync(timelineComponent);
        }

        public void CreateTimeLineComponent(TimelineComponent timelineComponent)
        {
            _PLContext.TimelineComponents.Add(timelineComponent);
        }

        public void UpdateTimeLineComponent(TimelineComponent timelineComponent)
        {
            _PLContext.TimelineComponents.Update(timelineComponent);
        }

        public async Task<TimelineComponent> GetTimelineComponentByIdAsync(int id)
        {
            return await _PLContext.TimelineComponents.Include(r => r.Timeline).SingleOrDefaultAsync(s => s.TC_Id == id);
        }

        public async Task RemoveTimelineComponentAsync(int id)
        {
            TimelineComponent timelineComponent = await _PLContext.TimelineComponents.FindAsync(id);
            _PLContext.TimelineComponents.Remove(timelineComponent);
        }

        public async Task<List<TimelineComponent>> GetTimelineComponentsAsync()
        {
            return await _PLContext.TimelineComponents.Include(r => r.Timeline).ToListAsync();
        }

        public async Task<List<TimelineComponent>> GetTimelineComponentsOfTimeLineAsync(int tlId)
        {
            return await _PLContext.TimelineComponents.Include(r => r.Timeline)
                .Where(w => w.TL_Id == tlId).ToListAsync();
        }
        public bool ExistTLC(int id)
        {
            return _PLContext.TimelineComponents.Any(a => a.TC_Id == id);
        }
        #endregion TimeLineComponent
        #region ContactMessage
        public void CreateMessage(ContactMessage contactMessage)
        {
            _PLContext.ContactMessages.Add(contactMessage);
        }
        public async Task<List<ContactMessage>> GetContactMessagesAsync()
        {
            return await _PLContext.ContactMessages.ToListAsync();
        }
        public async Task<ContactMessage> GetContactByIdAsync(int id)
        {
            return await _PLContext.ContactMessages.FindAsync(id);
        }

        public void UpdateMessage(ContactMessage contactMessage)
        {
            _PLContext.ContactMessages.Update(contactMessage);
        }
        #endregion
        #region General
        public async Task SaveChangesAsync()
        {
            await _PLContext.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _PLContext.SaveChanges();
        }





        #endregion
        #region ContactInfo
        public void CreateContactInfo(ContactInfo contactInfo)
        {
            _PLContext.ContactInfos.Add(contactInfo);
        }

        public void UpdateContactInfo(ContactInfo contactInfo)
        {
            _PLContext.ContactInfos.Update(contactInfo);
        }

        public async Task<ContactInfo> GetLastContactInfo()
        {
            return await _PLContext.ContactInfos.OrderByDescending(r=>r.CI_Id).FirstOrDefaultAsync();
        }

        public async Task<ContactInfo> GetContactInfoById(int id)
        {
            return await _PLContext.ContactInfos.FindAsync(id);
        }

        public void RemoveContactInfo(ContactInfo contactInfo)
        {
            contactInfo.IsDeleted = true;            
            _PLContext.ContactInfos.Update(contactInfo);
        }

        public async Task<List<ContactInfo>> GetContactInfosAsync()
        {
            return await _PLContext.ContactInfos.ToListAsync();
        }

        public bool ExistContactInfo(int id)
        {
            return _PLContext.ContactInfos.Any(a => a.CI_Id == id);
        }



        #endregion
        #region Header
        public void CreateHeader(Header header)
        {
            _PLContext.Headers.Add(header);
        }

        public void UpdateHeader(Header header)
        {
            _PLContext.Headers.Update(header);
        }

        public async Task<Header> GetHeaderByIdAsync(int id)
        {
            return await _PLContext.Headers.FindAsync(id);
        }

        public async Task<List<Header>> GetHeadersAsync()
        {
            return await _PLContext.Headers.ToListAsync();
        }

        public async Task<Header> GetLastHeader()
        {
            return await _PLContext.Headers.OrderByDescending(r => r.Header_id).FirstOrDefaultAsync();
        }

        public void RemoveHeader(int id)
        {
            Header header = _PLContext.Headers.Find(id);
            _PLContext.Headers.Remove(header);
        }

        public bool ExistHeader(int id)
        {
            return _PLContext.Headers.Any(a => a.Header_id == id);
        }
        #endregion
        #region Insta
        public void Create_Insta(InstaPost instaPost)
        {
            _PLContext.InstaPosts.Add(instaPost);
        }

        public void Edit_Insta(InstaPost instaPost)
        {
            _PLContext.Update(instaPost);
        }

        public async Task<List<InstaPost>> GetInstaPosts()
        {
            return await _PLContext.InstaPosts.ToListAsync();
        }

        public async Task<InstaPost> GetInstaPostWithId(int id)
        {
            return await _PLContext.InstaPosts.SingleOrDefaultAsync(s => s.InstaPostId == id);
        }
        public void RemoveInstaPost(int id)
        {
            InstaPost instaPost = _PLContext.InstaPosts.Find(id);
            _PLContext.InstaPosts.Remove(instaPost);
        }

        #endregion
        #region SiteFAQ
        public void CreateSiteFAQ(SiteFAQ siteFAQ)
        {
            _PLContext.SiteFAQs.Add(siteFAQ);
        }

        public void UpdateSiteFAQ(SiteFAQ siteFAQ)
        {
            _PLContext.SiteFAQs.Update(siteFAQ);
        }

        public async Task RemoveSiteFAQ(int id)
        {
            SiteFAQ siteFAQ =await _PLContext.SiteFAQs.FindAsync(id);
            _PLContext.SiteFAQs.Remove(siteFAQ);

        }

        public async Task<List<SiteFAQ>> GetSiteFAQs()
        {
            return await _PLContext.SiteFAQs.ToListAsync();
        }

        public async Task<SiteFAQ> GetSiteFAQById(int id)
        {
            return await _PLContext.SiteFAQs.FindAsync(id);
        }

        public bool ExistSiteFAQ(int id)
        {
            return _PLContext.SiteFAQs.Any(a => a.SiteFAQ_Id == id);
        }
        public async Task<List<SiteFAQ>> GetSiteFAQsWithoutRes()
        {
            return await _PLContext.SiteFAQs.Where(w => string.IsNullOrEmpty(w.SiteFAQ_Reply)).ToListAsync();
        }
        #endregion
        #region PageInfo
        public void CreatePageInfo(PageInfo pageInfo)
        {
            _PLContext.PageInfos.Add(pageInfo);
        }

        public void UpdatePageInfo(PageInfo pageInfo)
        {
            _PLContext.PageInfos.Update(pageInfo);
        }

        public async Task RemovePageInfo(int id)
        {
            PageInfo pageInfo = await _PLContext.PageInfos.FindAsync(id);
            _PLContext.PageInfos.Remove(pageInfo);
        }

        public async Task<List<PageInfo>> GetPageInfos()
        {
            return await _PLContext.PageInfos.ToListAsync();
        }

        public async Task<PageInfo> GetPageInfoById(int id)
        {
            return await _PLContext.PageInfos.FindAsync(id);
        }

        public bool ExistPageInfo(int id)
        {
            return _PLContext.PageInfos.Any(a => a.PInfo_Id == id);
        }

        public async Task<PageInfo> GetPageInfoByNameAsync(string name)
        {
            return await _PLContext.PageInfos.FirstOrDefaultAsync(f => f.PInfo_PageName.ToLower() == name);
        }
        #endregion
        #region PackInfo
        public void CreatePackInfo(PackInfo packInfo)
        {
            _PLContext.PackInfos.Add(packInfo);
        }

        public void UpdatePackInfo(PackInfo packInfo)
        {
            _PLContext.PackInfos.Update(packInfo);
        }

        public async Task RemovePackInfo(int id)
        {
            PackInfo packInfo = await _PLContext.PackInfos.FindAsync(id);
            _PLContext.PackInfos.Remove(packInfo);
        }

        public async Task<List<PackInfo>> GetPackInfos()
        {
            return await _PLContext.PackInfos.ToListAsync();
        }

        public async Task<PackInfo> GetPackInfoById(int id)
        {
            return await _PLContext.PackInfos.FindAsync(id);
        }

        public bool ExistPackInfo(int id)
        {
            return _PLContext.PackInfos.Any(a => a.PackInfo_Id == id);
        }

        public async Task<PackInfo> GetPackInfoByName(string name)
        {
            if(!string.IsNullOrEmpty(name))
            {
                return await _PLContext.PackInfos.OrderByDescending(r => r.PackInfo_Id).FirstOrDefaultAsync(f => f.PackInfo_PackName.ToLower() == name.ToLower());
            }
            else
            {
                return null;
            }
        }
        #endregion
        #region Separator
        public void CreateSeparator(Separator separator)
        {
            _PLContext.Separators.Add(separator);
        }

        public void UpdateSeparator(Separator Separator)
        {
            _PLContext.Separators.Update(Separator);
        }

        public async Task RemoveSeparator(int id)
        {
            Separator separator = await _PLContext.Separators.FindAsync(id);
            _PLContext.Separators.Remove(separator);
        }

        public async Task<List<Separator>> GetSeparators()
        {
            return await _PLContext.Separators.ToListAsync();
        }

        public async Task<Separator> GetSeparatorById(int id)
        {
            return await _PLContext.Separators.FindAsync(id);
        }

        public bool ExistSeparator(int id)
        {
            return _PLContext.Separators.Any(a => a.Id == id);
        }

        public async Task<Separator> GetSeparatorByName(string name)
        {
            return await _PLContext.Separators.FirstOrDefaultAsync(f => f.Name.ToLower() == name.ToLower());
        }
        #endregion
        #region EmailBank
        public void CreateEmailBank(EmailBank emailBank)
        {
            _PLContext.EmailBanks.Add(emailBank);
        }

        public async Task RemoveEmailBank(int id)
        {
            EmailBank emailBank = await _PLContext.EmailBanks.FindAsync(id);
        }

        public async Task<List<EmailBank>> GetEmailBanks()
        {
            return await _PLContext.EmailBanks.ToListAsync();
        }

        public async Task<EmailBank> GetEmailBankByIdAsync(int id)
        {
            return await _PLContext.EmailBanks.FindAsync(id);
        }

        public bool ExistEmailBank(int id)
        {
            return _PLContext.EmailBanks.Any(a => a.EBId == id);
        }

        public async Task<bool> ExistEmail(string email)
        {
            return await _PLContext.EmailBanks.AnyAsync(a => a.EBEmail.Trim().ToLower() == email.ToLower().Trim());
        }

        #endregion
    }
}
