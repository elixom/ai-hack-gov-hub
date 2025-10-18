@component('mail::message')
# Welcome to {{ config('app.name') }}

Hello {{ $data['user'] }},

Our new platform is for managing clinical operations efficiently. This system is designed to help us coordinate appointments, access patient records securely, and manage administrative tasks.

**Next Steps:**
- Log in using your credentials.
- Go to your profile and update your password.
- Set up your two-factor authentication for enhanced security.
- Review your dashboard to see schedules, patient records, and notifications.
- Familiarize yourself with the system’s features to streamline daily operations.


@component('mail::panel')
**Temporary Password:** {{ $data['tempPassword'] }}
@endcomponent

@component('mail::button', ['url' => config('app.url') . '/login'])
Log In to the System
@endcomponent

If you encounter any issues or have questions, please contact our support team.

Thanks,
{{ config('app.name') }}
@endcomponent
