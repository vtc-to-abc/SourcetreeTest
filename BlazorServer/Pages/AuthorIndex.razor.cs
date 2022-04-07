using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLibary.Models;
using DataAccessLibary;
using BlazorServer.Data;
using Microsoft.AspNetCore.Components;
using DataAccessLibary;

namespace BlazorServer.Pages
{
    public partial class AuthorIndex: ComponentBase
    {
        [Inject] private IAuthorData AuthorDB { get; set; }
        [Parameter] public int TestComponentParemeter { get; set; }
     private List<AuthorModel> authors = new List<AuthorModel>();

    protected override async Task OnInitializedAsync()
    {
        authors = await GetData();
         await base.OnInitializedAsync();
        }

        public async Task<List<AuthorModel>> GetData()
        {
            AuthorModel detailtest = new AuthorModel
            {
                author_id = 1,
            };
            if (TestComponentParemeter == 1)
                return await AuthorDB.SearchAuthor(detailtest);
            else
                return await AuthorDB.GetAuthor();
        }
        public async Task InsertNewAuthor()
        {
            AuthorModel insertest = new AuthorModel
            {
                pseudonym = "test insert"
            };
            await AuthorDB.InsertAuthor(insertest);
        }

        public async Task EditAuthorInformation()
        {
            AuthorModel edittest = new AuthorModel
            {
                author_id = 1,
                pseudonym = "test edit"
            };
            await AuthorDB.EditAuthor(edittest);
        }
        public async Task DeleteAuthor(AuthorModel author )
        {
            await AuthorDB.Delete(author);
        }
    }
}
