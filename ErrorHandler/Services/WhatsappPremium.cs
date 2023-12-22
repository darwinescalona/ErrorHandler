
using ErrorHandler.Config;

namespace ErrorHandler.Services;

public class WhatsappPremium
{
    public async Task<string> GetSellerInfoAsync(string seller)
    {
        var client = new HttpClient();
        var response = await client.GetAsync($"https://wa-registration-api.prod.notif.csnglobal.net/v1/Account/{seller}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    // Ejemplo: No retornar data y usar excepciones propias 
    public async Task<string> QueryGetSellerInfoAsync(string seller)
    {
        try
        {
            string sellerInfo = await GetSellerInfoAsync(seller);
            if (sellerInfo == null)
            {
                throw new DataNotFoundException("Seller not found");
            }
            return sellerInfo;
        }
        catch (Exception ex)
        {
            // Ejemplo: Uso solo del throw para mantener trazabilidad del error
            throw;
        }
    
    }

}
