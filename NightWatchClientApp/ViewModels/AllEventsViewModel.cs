using CommunityToolkit.Maui.Views;
using NightWatchClientApp.Data.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightWatchClientApp.ViewModels;

public partial class AllEventsViewModel: ObservableObject
{
    public List<EventModel> AllEvents;

    [ObservableProperty] private ObservableCollection<EventModel> eventList;
    [ObservableProperty] public bool isRefreshing = false;
    [ObservableProperty] public string errorMessage = "";

    private readonly IEventData _eventData;

    public AllEventsViewModel(IEventData eventData)
    {
        _ = LoadEvents();
        _eventData = eventData;

    }

    [RelayCommand]
    public async Task LoadEvents()
    {
        IsRefreshing = true;
        try
        {
            await Task.Delay(1000);
            //var image = "https://avatars.mds.yandex.net/i?id=0bd48196c8a84bedbb1aa839e70cf91916235a61-7570837-images-thumbs&n=13";
            //EventList = new()
            //{
            //    new EventModel(
            //        Name: "First",
            //        Desciption: "firstEvent",
            //        Image: image),
            //    new EventModel(
            //        Name: "Second",
            //        Desciption: "secondEvent",
            //        Image: image),

            //};

            AllEvents = await _eventData.GetAllEvents();
            EventList = new ObservableCollection<EventModel>(AllEvents);
        }
        catch (Exception ex)
        {

            ErrorMessage = ex.Message;
        }
        finally
        {
            IsRefreshing = false;
        }
        
    }


    [RelayCommand]
    private async Task GoToDetails(EventModel selectedEvent)
    {
        if (selectedEvent != null)
        {
            await Shell.Current.GoToAsync(nameof(EventDetailsPage), true, new Dictionary<string, object>
            {
                {nameof(EventModel), selectedEvent }
            });
        }
    }



    //private void TextChangedOnSearchBar(object sender, TextChangedEventArgs e)
    //{
    //    myListEvents.ItemsSource = EventList.Where(s => s.Name.StartsWith(e.NewTextValue));
    //}

    public void TextChangedCommand(string data)
    {
        EventList = new(AllEvents.Where(x => x.name.StartsWith(data)).ToList());
    }

    //private void Button_Clicked(object sender, EventArgs e)
    //{
    //    var image = "https://avatars.mds.yandex.net/i?id=0bd48196c8a84bedbb1aa839e70cf91916235a61-7570837-images-thumbs&n=13";
    //    EventList.Add(new EventModel("catacomby", "this is quest with some real shit", image));
    //}

    //private void Button_Clicked_1(object sender, EventArgs e)
    //{
    //    if (EventList.Count > 1)
    //        EventList.RemoveAt(1);
    //}

}
