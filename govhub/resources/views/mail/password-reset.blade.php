@component('mail::message')
# Your Temporary Password

Hello {{ $data['user'] }},

We’ve created a temporary password for your account.
Use the credential below to log in:

@component('mail::panel')
**Temporary Password:** {{ $data['tempPassword'] }}
@endcomponent

⚠️ For your security, please change your password immediately after logging in.

@component('mail::button', ['url' => config('app.url') . '/login'])
Log In
@endcomponent

If you did not request this password, please contact our support team immediately.

Thanks,
{{ config('app.name') }}
@endcomponent
