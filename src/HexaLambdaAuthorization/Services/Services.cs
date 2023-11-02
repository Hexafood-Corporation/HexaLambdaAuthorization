using Amazon;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;

namespace HexaLambdaAuthorization.Services
{
    public class Services
    {
        public async Task<string> GetParametersSSM(string parameterName)
        {
            using (var ssmClient = new AmazonSimpleSystemsManagementClient(RegionEndpoint.USEast1))
            {
                var request = new GetParameterRequest
                {
                    Name = parameterName,
                    WithDecryption = true
                };

                try
                {
                    var response = await ssmClient.GetParameterAsync(request);
                    return response.Parameter.Value;

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao recuperar o valor do SSM: {ex.Message}");
                }
            }

            return String.Empty;
        }
    }
}
