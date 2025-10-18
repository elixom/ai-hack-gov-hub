Project Generation Prompt for GOJ Hub Assistant
Project Name: GOJ Hub Assistant Core Goal: Build a real-time, chat-first assistant in C#/.NET 9 that guides users through Jamaican government processes, providing conversational guidance, checklists, and document discovery, leveraging Azure OpenAI for natural inquiry routing.

Remember to obey the clinerules and review the files in the docs folder for a detailed understanding of the objective project.

1. Technology Stack & Foundation
Backend: C# .NET 9 Web API.

Frontend: Blazor WebAssembly (or Blazor Server, prioritize Wasm if possible for a true SPA, but ensure SignalR connectivity). The entire application must be a Single-Page Web App (SPA).

Real-time Communication: Use ASP.NET Core SignalR (USE LOACALLY HOSTED SIGNALR) for real-time push updates (NFR-RT: task.created, task.updated, tracker.updated, reminder.due).

AI Integration: Use the Azure OpenAI Service SDK (or equivalent HttpClient integration) to connect to a deployed GPT-4o (or similar) model.

Styling/UX: Use Tailwind CSS or a modern C#-compatible UI library (like MudBlazor) to create an excellent, responsive chat UI with a fixed-width main chat area and a pinned right rail context panel.

2. LLM Role & AI Orchestration (FR-1, CE-1)
The AI model's primary function is Intent Classification and Routing.

System Prompt: The model should be instructed to act as the "GOJ Hub Assistant," a helpful and knowledgeable guide for Jamaican government services.

Tool/Function Calling Mock: Define a set of internal tools (mocked functions/endpoints) that the LLM must choose from based on the user's message.

ServiceSearch(query: string)

StartWizard(service_id: string)

ShowDocuments(service_id: string)

QuickAttachTracker(provider: string, reference: string)

AddReminder(offset: string)

GeneralQuery(query: string) (Fallback for non-service-related questions)

Orchestrator Logic: The main API endpoint (/api/chat) receives the user message. It calls Azure OpenAI for a structured JSON response indicating which tool to use. The C# backend then executes the appropriate business logic (e.g., querying the Service Catalog for a match, or starting the Wizard flow).

3. Data Model (C# Classes)
Define C# record types or classes for the following core entities:

Service: Id, Title, ShortDescription, Tags, CanonicalDeepLink, WizardTemplateId.

Task: Id, UserId, ServiceId, Status (e.g., PENDING, IN_PROGRESS, COMPLETE), ProgressPercent, Deadline, AttachedDocuments (list of URLs).

ChecklistStep: Description, IsCompleted, RequiredDocuments.

ChatMessage: Id, Text, Role (User / Assistant / System), Timestamp, and a RichContent JSON payload (to hold structured data like a list of service chips, a checklist card, or a status pill).

4. UI/UX Components (Blazor)
The application structure must consist of two main, responsive panels:

Left Panel (Main Chat Area):

Display all ChatMessage objects.

Must support Rich Message Types (based on the RichContent payload):

Service Match Chips (CE-1): Clickable buttons/chips to select a service.

Wizard Quick-Reply Chips (CE-2): 3-6 quick-response buttons within the chat flow.

Checklist Card (FR-3): A structured card showing ordered steps with toggle buttons. Toggling must trigger a SignalR update.

Tracker Status Pill (CE-3): A compact, color-coded status update.

Document Carousel (CE-4): Horizontal list of document cards.

Activity Notes (CE-5): Small, system-style messages (e.g., "Opened TAJ link").

Input Box: Must process quick commands (/new, /docs, /track, /remind) as per FR-8.

Right Panel (Pinned Context Rail / FR-7):

This panel should be sticky and always visible (on desktop; perhaps collapsible on mobile).

Display the Current Task Card (Task.Status, Task.ProgressPercent, Task.Deadline with a live countdown).

Include tabs for: Checklist, Tracker, Reminders, and Attached Documents. All tabs must reflect real-time updates pushed via SignalR.

5. Mock Data & Implementation Detail
Service Catalog (Mock): Create a static C# list of at least 3 mock services (Vehicle Fitness & Registration, NHT Benefits, TAJ/Taxes) to seed the ServiceCatalog (FR-2).

Real-Time Trackers (Mock): Implement a simple TrackerService (FR-4) that uses a timer loop (or a dedicated "Force Update" button) to simulate status changes for MOCK_FITNESS and MOCK_NHT tracker providers. Status changes must be broadcast via SignalR.

Document Finder (Mock): Create a mock DocumentFinderService (FR-6) that returns a list of 2-6 relevant mock links/PDFs when queried (e.g., "Show documents for NHT benefits"). The UI must support a side-viewer preview mock.

DELIVERABLE:
Provide the complete C#/.NET 9 Blazor solution (Program.cs, relevant Service and Component files) in a single file block, demonstrating:

The structured C# data models.

The Azure OpenAI/Orchestrator integration mock (simulating the intent choice).

A responsive Chat UI and Right Rail using Blazor components and styling.

The SignalR hub setup and basic client-side subscription to real-time updates.