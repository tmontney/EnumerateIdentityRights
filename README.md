# EnumerateIdentityRights
## Overview
While working on another project, I was told to provide a service account
[Act as part of the operating system](https://docs.microsoft.com/en-us/windows/security/threat-protection/security-policy-settings/act-as-part-of-the-operating-system)
because it's a background service. Now I know background services require special rights (as I've written a background service or two), but in absolutely no way does it
require this privilege. (I might as well run it as SYSTEM!) The installer is supposed to assign necessary rights anyway which does work under certain conditions. And since
it works, it concerned me that it may be assigning the service account this privilege.

Enter EnumerateIdentityRights.

## Identity?
I use identity to mean user or group.

## EnumerateIdentityRights-UI
Operation is quite simple: Run the program and it will list the privileges of a user.
- EnumerateIdentityRights-UI.exe
  - Gets the assigned rights of the current identity
- EnumerateIdentityRights-UI.exe Administrator
  - Gets the assigned rights of identity 'Administrator'
  - Again, the identity can be a user or a group (and possibly other things, too)

## Credit
I wouldn't have been able to do it without these resources:
- https://web.archive.org/web/20151218044501/http://www.hightechtalks.com/csharp/lsa-functions-276626.html
  - Original "author", which most of this library is based off. 
- https://docs.microsoft.com/en-us/windows/win32/api/ntsecapi/nf-ntsecapi-lsaenumerateaccountrights
- https://www.powershellgallery.com/packages/cUserRightsAssignment/1.0.0.0/Content/DSCResources%5CcUserRight%5CcUserRight.psm1
  - Helped explain the ambiguous `LSA_UNICODE_STRING_withPointer` from https://www.pinvoke.net/default.aspx/advapi32.lsaenumerateaccountrights
- https://stackoverflow.com/a/14469248/1340075
  - HTT code contains a flaw that releases memory too early, resulting in unpredictable behavior of `LsaEnumerateAccountRights`
