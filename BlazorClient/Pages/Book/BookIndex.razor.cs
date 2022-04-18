using Microsoft.AspNetCore.Components;
using BlazorClient.Models;
using BlazorClient.Services;
using Microsoft.JSInterop;
namespace BlazorClient.Pages.Book
{
    public partial class BookIndex: ComponentBase
    {
        [Inject] private IBookService BookService { get; set; }
        [Inject] private IAuthorService AuthorService { get; set; }
        [Inject] private IAuthorBookService AuthorBookService { get; set; }
        [Inject] IJSRuntime JS { get; set; }

        protected IEnumerable<BookModel> Books { get; set; } = new List<BookModel>();

        private BookInsertModal _refBookInsertModal = new();
        private BookEditModal _refBookEditModal = new();
        private BookDeleteModal _refBookDeleteModal = new();

        private BookModel CurrentBook;
        private IEnumerable<AuthorModel> CurrentBookRelationalAuthors { get; set; } = new List<AuthorModel>();
        string testMergeConflict2 = "yes";

        public async Task GetCurrrentBook(BookModel book)
        {
             CurrentBook = book;
        }

        public async Task GetCurrrentRelationalAuthor(BookModel book)
        {
            await GetCurrrentBook(book);
            CurrentBookRelationalAuthors = await AuthorBookService.GetAuthorsByBook(book);
        }


        protected override async Task OnInitializedAsync()
        {
            Books = await GetData();
            await base.OnInitializedAsync();
        }

        public async Task<IEnumerable<BookModel>> GetData()
        {
            return await BookService.GetBooks();
            StateHasChanged();

        }
        public async Task InsertNewBook()
        {
            await _refBookInsertModal.SetVisible(true);

            StateHasChanged();
        }

        public async Task EditBookInformation()
        {
            
            await _refBookEditModal.SetVisible(true);
            StateHasChanged();

        }
        public async Task DeleteBook()
        {
            await _refBookDeleteModal.SetVisible(true);
            StateHasChanged();

        }
    }
}
