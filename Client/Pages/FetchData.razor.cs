using FormationBlazor.Shared;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace FormationBlazor.Client.Pages
{
    public partial class FetchData
    {
        //@inject HttpClient Http
        [Inject] private HttpClient Http { get; set; }

        private Book[]? currentBook;
        private List<Book> allBooks;
        private bool _isLoading = true;

        private IDisposable? registration;

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                registration =
                    NavigationManager.RegisterLocationChangingHandler(OnLocationChanging);
            }
        }

        protected override async Task OnInitializedAsync()
        {

            allBooks = await Http.GetFromJsonAsync<List<Book>>("api/Books");
            currentBook = allBooks.ToArray();
            _isLoading = false;
        }

        private ValueTask OnLocationChanging(LocationChangingContext context)
        {

            if (context.TargetLocation == "/counter")
                context.PreventNavigation();

            return ValueTask.CompletedTask;

        }

        private void SearchChange(string search)
        {
            currentBook = allBooks.Where(p => p.Name.Contains(search)).ToArray();
            StateHasChanged();
        }

        private void AddBook()
        {
            NavigationManager.NavigateTo("/book-add");
        }

        public void Dispose() => registration?.Dispose();
    }
}
