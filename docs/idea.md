GOJ Hub - Chat UI + Docs Ingestion (24h MVP Requirements)
1) Product Vision

    A chat-based assistant that helps Jamaicans find, follow, and finish government tasks (e.g., vehicle fitness/registration → NHT → taxes), with:

    Conversational guidance + checklists

    Real-time task/tracker updates

    Built-in browsing of official documents/links pulled from public GOJ sites

2) Scope (MVP)
In Scope

    Chat-first workflow to discover services, generate checklists, attach trackers, and set reminders.

    Inline results: step cards, deep links, preview of relevant documents.

    Docs ingestion (public URLs only): seed a small set of GOJ domains, fetch + index page/PDF links, expose in chat search.

    Real-time push for tracker/reminder updates in the chat thread.

    Single-page web app (no native).

Out of Scope (MVP)

    Logging into GOJ systems, scraping behind auth, or handling PII beyond user email and task metadata.

    Payments, advanced analytics, sophisticated crawler scheduling.

3) Core Chat Experience
CE-1: Natural Inquiry → Service Match

    User message: "How do I renew fitness and registration, then check NHT benefits?"

    Assistant returns:

        Service match chips (e.g., Vehicle Fitness & Registration, NHT Benefits).

        Short descriptions + primary deep links.

        CTA buttons: "Start checklist", "Show documents", "Add tracker".

Acceptance: Selecting a service chip inserts a structured assistant card and advances the flow without leaving chat.
CE-2: Wizard-in-Chat

    Assistant asks 3–6 guided questions (parish, vehicle type, ownership, etc.) as quick-reply chips or compact forms inside messages.

    On completion, assistant posts:

        Checklist card (ordered steps + docs + fees)

        Deadline chip + live countdown

        Create Task button (posts a "Task created" system message when done).

Acceptance: Checklist appears as a rich message; countdown starts immediately in the message bubble.
CE-3: Trackers & Reminders in Chat

    Message action "Attach tracker" opens a minimal inline form (provider + reference).

    Assistant confirms and posts a status pill inside the thread.

    Reminders added via quick commands (e.g., Remind me 7 days before) or a button.

    Background loop triggers real-time updates as system messages (e.g., "Tracker moved to AWAITING_PAYMENT").

Acceptance: A status change appears as a new message without page refresh.
CE-4: Document & Link Discovery in Chat

    User types: "Show documents for NHT benefits" or clicks "Show documents".

    Assistant returns a documents carousel (title, source domain, type: PDF/HTML, short snippet).

    Clicking opens in a side viewer (PDF/HTML preview if possible) or in a new tab.

    Provide "Save to Task" action to pin a doc to the current task.

Acceptance: At least 6 relevant docs/links returned within the seeded sites; user can preview or attach to task directly.
CE-5: Deep Links & Activity Notes

    Assistant message includes primary official link(s) with "Open" and "Copy link" actions.

    When clicked, assistant posts a small activity note message ("Opened TAJ link").

Acceptance: Links open in new tab; activity note appears instantly.
4) Functional Requirements
FR-1: Chat Orchestrator

    Parses messages → routes to capabilities:

        Service Search Tool

        Wizard/Checklist Tool

        Tracker Tool (mock adapters)

        Reminder Tool

        Document Finder Tool (index + search)

        Deep Link Tool

    Supports quick-reply chips and inline forms within messages.

Acceptance: All flows are performable without leaving the chat thread.
FR-2: Service Catalog (seeded)

    Minimum 3 services: Vehicle Fitness & Registration, NHT Benefits, TAJ/Taxes.

    Each service has: title, short description, tags, canonical deep links, and a wizard template (questions, rules).

    Local search surfaces services from user text.

Acceptance: Typing "fitness" or "NHT" surfaces correct services in ≤150ms.
FR-3: Checklist Generation

    Based on wizard answers, create a step-by-step checklist with:

        Steps (≥4), required documents list, static fee estimates, deadline rules.

    Present as a rich checklist message with toggles; Create Task posts a "Task created" message and pins a summary card to the right rail.

Acceptance: Toggling a step updates progress (percent) in the message and pinned card.
FR-4: Real-Time Trackers (Mock)

    Providers: MOCK_FITNESS, MOCK_NHT, MOCK_TAX.

    Poll every 30–60s or via "Force Update" button in the chat (for demo).

    On state change, post a system message with the new status and update pinned card.

Acceptance: Status transitions appear within 2s after Force Update.
FR-5: Reminders

    Add reminders via quick command ("Remind me 1 hour before deadline") or button.

    When due, post a reminder message and highlight the task in the pinned card.

Acceptance: Setting "in 1 minute" produces a reminder message during demo.
FR-6: Document Finder Tool (public URLs only)

    Seeded domains list (small set of official GOJ sites) configurable via env.

    On-demand fetch (no heavy crawl): fetch landing pages + follow obvious document links (same-domain), index:

        URL, title, snippet/first text, MIME (HTML/PDF), filesize/last-modified if available.

    Index & Search: simple text search over title/snippet/url; rank by keyword match.

    Deduplicate by normalized URL; respect robots.txt; rate-limit requests.

    Preview: inline PDF/HTML preview if feasible, else new tab.

Acceptance: "Show documents for [service]" yields ≥6 links from seeded domains with basic relevance; results are clickable + previewable.
FR-7: Pinned Right Rail (Context Panel)

    Shows current Task Card (status, countdown, progress).

    Tabs: Checklist, Tracker, Reminders, Documents (attached).

    Updates live when the chat triggers changes.

Acceptance: Changes from chat reflect immediately in the panel.
FR-8: Commands (quality-of-life)

    /new start a new service flow.

    /docs <query> search docs immediately.

    /track <provider> <ref> quick-attach tracker.

    /remind <offset> add reminder quickly.

    Provide clickable help message listing commands.

Acceptance: Commands work equivalently to button-driven flows.
5) Non-Functional Requirements
NFR-RT (Real-Time)

    WebSocket push for: task.created, task.updated, tracker.updated, reminder.due, docs.indexed.

    Client should reconnect gracefully and replay missed updates (simple refetch on connect).

NFR-Perf

    Service search ≤150ms (local).

    Chat message send → tool reaction → response message ≤800ms (excluding external fetch).

    Docs fetch rate-limited (e.g., ≤1 req/sec per domain) with max 10 URLs per service in MVP.

NFR-Security & Compliance

    No credentials to government systems.

    Only public pages; respect robots.txt; add a polite User-Agent string.

    Sanitize user inputs and fetched snippets; block inline script execution in previews.

    Make origins/domains transparent in the UI ("Source: taj.gov.jm", etc.).

NFR-Privacy

    Store only user's task metadata, reminders, and manual tracker references.

    Clearly indicate that the product does not represent the GOJ; it links to official sites.

NFR-UX & Accessibility

    All actions possible via keyboard; aria-live announcements for new messages, countdown updates, and toasts.

    Status colors meet contrast ratios; link cards show file type (PDF/HTML) and source domain.

NFR-Resilience

    If docs fetch fails, show a friendly fallback message with the deep links still available.

    Force Update remains available to keep demo deterministic.