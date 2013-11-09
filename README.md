# CsrFence

CSRFence is simple library for ASP.NET WebForms, offering protection from CSRF (_Cross-Site Request Forgery_) attacks. ASP.NET Web Forms are in newer version protected by default, but the protection requires WiewState to be enabled.

There are some other solutions, but they're usually unnecesarilly complex to install and configure or are dependent on ViewState or SessionState to store values or generate session identifiers.

## How to install

Use NuGet Package manager and install the `Altairis.CsrFence` package trough GUI or trough the Package Manager Console using the following command:

```
Install-Package Altairis.CsrFence
```

## How to configure

In most cases, no configuration is necessary. CSRFence uses sensible defaults that you would probably never need to modify. But, there are several options to configure, if you would really want to. The following is default configuration:

```xml
<configuration>
	<configSections>
		<section name="altairis.csrFence" type="Altairis.CsrFence.Configuration.CsrFenceSection "/>
	</configSections>
	<altairis.csrFence>
		<sessionId length="64" cookieName=".CSRFENCE" />
		<token fieldName="__CSRFTOKEN" purposeString="Altairis.CsrFence.ProtectionModule.Token" />
	</altairis.csrFence>
</configuration>
```

* `sessionId` - options for generating random session ID
	* `length` - length of ID in bytes, defaults to 64 B (512 bits)
	* `cookieName` - name of the session ID cookie, defauls to `.CSRFENCE`
* `token` - options for the hidden field token 
	* `fieldName` - hidden field name, defaults to `__CSRFTOKEN`
	* `purposeString` - key derivation string used by `MachineKey.Protect` method

## How it works

This library uses _Synchronizer Token Pattern_ for protection against CSRF attacks. On first request, the user is issued random value (it's kind of session ID, but different from SesstionID used by SessionState and independent on it). This value is stored in cookie.

Each subsequent POST request contains hidden field, whose value is cryptographically derived from the session ID using the same methods how ViewState, ControlState and authentication tokens are protected. If the value is missing or invalid, CSRFence throws the `HttpRequestValidationException`.

## Requirements and limitations

* CSRFence requires .NET Framework 4.5 and higher.
* Only ASP.NET Web Forms are protected. The module does not work for ASP.NET MVC or Web Pages, there are other ways for those.

## Contact author

* Web site: http://www.rider.cz/
* Blog (in Czech): http://www.aspnet.cz/
* Facebook: rider.cz
* Twitter: @ridercz
* E-mail: michal.valasek (at) altairis.cz

## Licensing

This is a free software, licensend uder terms of the MIT License.

Copyright (c) 2013 Michal Altair Valasek

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.