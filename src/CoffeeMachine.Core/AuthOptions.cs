namespace CoffeeMachine.Core;

using System.Text;

using Microsoft.IdentityModel.Tokens;

/// <summary>
/// ����� ��� �������� �������� ��������� jwt ������.
/// </summary>
public static class AuthOptions
{
    /// <summary>
    /// ����������� ������.
    /// </summary>
    public const string AUDIENCE = "MyAuthClient";

    /// <summary>
    /// �������� ������.
    /// </summary>
    public const string ISSUER = "MyAuthServer";

    /// <summary>
    /// ���� ��� ��������.
    /// </summary>
    private const string KEY = "mysupersecret_secretkey!123";

    /// <summary>
    /// �������� ���������� ����.
    /// </summary>
    /// <returns> ������ ����, ��������� �� �����. </returns>
    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}