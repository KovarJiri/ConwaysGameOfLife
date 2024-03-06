using ConwaysGameOfFile.DTO;
using System.Net.Http.Json;
using System.Text.Json;

namespace ConwaysGameOfLife.Common.API
{
    public partial class BoardService
    {
        public class BoardApi
        {
            private readonly HttpClient _httpClient = null!;
            private readonly JsonSerializerOptions? jSONSerializeOptions = null!;

            public BoardApi(HttpClient httpClient)
            {
                _httpClient = httpClient;

                JsonSerializerOptions? jSONSerializeOptions = new System.Text.Json.JsonSerializerOptions();
                Settings.JSONSerializeOptions.SetSerializationOptions(jSONSerializeOptions);
            }

            #region GET
            
            public virtual Task<Guid> GetNewBoard(int boardXSize = 500, int boardYSize = 500, int boardType = 0, CancellationToken cancellationToken = default) 
            {
                return _httpClient.GetFromJsonAsync<Guid>($"api/v1/board/{boardXSize}/{boardYSize}/{boardType}", jSONSerializeOptions, cancellationToken);
            }
            public virtual Task<BoardDto> GetActualBoard(Guid guidOfBoardToEvolve, CancellationToken cancellationToken = default)
            {
                return _httpClient.GetFromJsonAsync<BoardDto>($"api/v1/board/{guidOfBoardToEvolve}", jSONSerializeOptions, cancellationToken);
            }

            #endregion

            #region POST

            public virtual async Task<BoardDto> EvolveBoard(Guid guidOfBoardToEvolve, CancellationToken cancellationToken = default)
            {
                var response = await _httpClient.PostAsJsonAsync<Guid>($"api/v1/board", guidOfBoardToEvolve, jSONSerializeOptions, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.StatusCode.ToString());
                }

                return await response.Content.ReadFromJsonAsync<BoardDto>();
            }

            #endregion
        }
    }
}
