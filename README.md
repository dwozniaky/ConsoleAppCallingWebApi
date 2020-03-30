# Console App call .NET Framework Web API secured by Microsoft Identity platform

This is the simple project of web API and console app secured and configured on Azure AD based on MSC samples and documentation. ('Client Credentials Flow') 

## Installation

1. Git clone repo

```bash
git clone https://github.com/dwozniaky/ConsoleAppCallingWebApi.git
```
2. Register Web API application in your Azure Active Directory and add custom Role for daemon application
3. Register Client applicaiton in your Azure Active Directory and add scope for daemon application of your Web API registered application
4. Replace **ClientId** in **web.config** file with your **ClientId of Web API registered application** and replace **Tenant** with your **TenantId** in **Startup.Auth.cs** Web API class
5. Replace **ClientId, ClientSecret, Authority**(only Tenant part)**, ApiScope** in **Program.cs** Console app class
6. Select multiple startup projects and run both apps

## Docs and references

[https://docs.microsoft.com/bs-latn-ba/azure/active-directory/develop/scenario-daemon-overview](https://docs.microsoft.com/bs-latn-ba/azure/active-directory/develop/scenario-daemon-overview)
[https://docs.microsoft.com/bs-cyrl-ba/azure/active-directory/develop/scenario-protected-web-api-verification-scope-app-roles](https://docs.microsoft.com/bs-cyrl-ba/azure/active-directory/develop/scenario-protected-web-api-verification-scope-app-roles)

[https://docs.microsoft.com/bs-cyrl-ba/azure/active-directory/develop/scenario-protected-web-api-overview](https://docs.microsoft.com/bs-cyrl-ba/azure/active-directory/develop/scenario-protected-web-api-overview)
[https://docs.microsoft.com/bs-cyrl-ba/azure/active-directory/develop/scenario-protected-web-api-app-registration](https://docs.microsoft.com/bs-cyrl-ba/azure/active-directory/develop/scenario-protected-web-api-app-registration)
[https://docs.microsoft.com/bs-cyrl-ba/azure/active-directory/develop/scenario-daemon-app-registration](https://docs.microsoft.com/bs-cyrl-ba/azure/active-directory/develop/scenario-daemon-app-registration)



## License
[MIT](https://choosealicense.com/licenses/mit/)