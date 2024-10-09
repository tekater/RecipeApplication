public async Task<IActionResult> OnPostAsync(string returnUrl = null)
{
if (ModelState.IsValid)
{
var user = new ApplicationUser {
UserName = Input.Email, Email = Input.Email };
var result = await _userManager.CreateAsync(
user, Input.Password);
if (result.Succeeded)
{
var claim = new Claim("FullName", Input.Name);
await _userManager.AddClaimAsync(user, claim);
var code = await _userManager
.GenerateEmailConfirmationTokenAsync(user);
await _emailSender.SendEmailAsync(
Input.Email, "Confirm your email", code );
await _signInManager.SignInAsync(user);
return LocalRedirect(returnUrl);
}
foreach (var error in result.Errors)
{
ModelState.AddModelError(string.Empty, error.Description);
}
}
return Page();
}