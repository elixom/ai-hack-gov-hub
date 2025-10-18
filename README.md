# GOJ Hub Assistant

A real-time, chat-first assistant for Jamaican government services built with C# .NET 9, Blazor WebAssembly, and SignalR.

## Overview

GOJ Hub Assistant provides conversational guidance through Jamaican government processes, including:
- **Vehicle Fitness & Registration** - Motor vehicle inspection and licensing
- **NHT Benefits & Loans** - National Housing Trust services
- **TAJ Registration & Tax Compliance** - Tax Administration Jamaica services

### Key Features

- 🤖 **AI-Powered Intent Classification** - Natural language processing to understand user queries
- 💬 **Real-Time Chat** - Instant responses with SignalR push updates
- 📋 **Task Management** - Track progress with checklists and deadlines
- 🔍 **Application Tracking** - Monitor status of government applications
- ⏰ **Smart Reminders** - Set reminders for important deadlines
- 📱 **Responsive Design** - Works on desktop and mobile devices

## Architecture

### Projects

- **GovHub-lib** - Shared library containing models, DTOs, enums, and service interfaces
- **GovHub-api** - ASP.NET Core Web API backend with SignalR hub
- **GovHub-app** - Blazor WebAssembly frontend (SPA)

### Technology Stack

- **.NET 9.0** - Latest .NET framework
- **Blazor WebAssembly** - Client-side SPA framework
- **ASP.NET Core SignalR** - Real-time web functionality
- **MudBlazor** - Material Design component library
- **Azure OpenAI** (simulated) - AI intent classification with pattern matching

## Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Visual Studio 2022 or VS Code with C# Dev Kit
- (Optional) Azure OpenAI API key for production AI features

### Running the Application

1. **Clone the repository**
   ```bash
   git clone https://github.com/elixom/ai-hack-gov-hub.git
   cd ai-hack-gov-hub
   ```

2. **Run the API (Backend)**
   
   Open a terminal and navigate to the API project:
   ```bash
   cd src/GovHub-api
   dotnet run
   ```
   
   The API will start at `https://localhost:7001`

3. **Run the Blazor App (Frontend)**
   
   Open another terminal and navigate to the app project:
   ```bash
   cd src/GovHub-app
   dotnet run
   ```
   
   The app will start at `https://localhost:5001`

4. **Access the Application**
   
   Open your browser and navigate to `https://localhost:5001`

### Building for Production

```bash
# Build all projects
dotnet build

# Publish the API
cd src/GovHub-api
dotnet publish -c Release -o ./publish

# Publish the Blazor app
cd ../GovHub-app
dotnet publish -c Release -o ./publish
```

## Project Structure

```
ai-hack-gov-hub/
├── docs/
│   └── idea.md                 # Detailed requirements and specifications
├── src/
│   ├── GovHub-api/             # Web API Backend
│   │   ├── Controllers/
│   │   │   └── ChatController.cs
│   │   ├── Hubs/
│   │   │   └── ChatHub.cs     # SignalR hub for real-time updates
│   │   ├── Services/
│   │   │   └── ChatOrchestrator.cs
│   │   └── Program.cs
│   │
│   ├── GovHub-app/             # Blazor WebAssembly Frontend
│   │   ├── Pages/
│   │   │   ├── Chat.razor     # Main chat interface
│   │   │   └── Chat.razor.css
│   │   ├── Layout/
│   │   └── Program.cs
│   │
│   └── GovHub-lib/             # Shared Library
│       ├── Models/
│       │   ├── ChatMessage.cs
│       │   ├── Service.cs
│       │   ├── TaskEntity.cs
│       │   ├── Tracker.cs
│       │   └── AI/
│       │       └── ToolChoice.cs
│       ├── DTOs/
│       │   ├── ChatRequest.cs
│       │   └── ChatResponse.cs
│       ├── Enums/
│       │   ├── MessageRole.cs
│       │   ├── RichContentType.cs
│       │   ├── TaskStatus.cs
│       │   └── TrackerStatus.cs
│       └── Services/
│           ├── AIOrchestrationService.cs
│           ├── ServiceCatalogService.cs
│           ├── TrackerService.cs
│           └── ReminderService.cs
```

## Features

### Chat Interface

The chat interface supports:
- **Natural Language Queries** - Ask questions in plain English
- **Service Chips** - Clickable service suggestions
- **Quick Replies** - Pre-defined response options
- **Typing Indicators** - Real-time feedback
- **Command Shortcuts** - `/new`, `/docs`, `/track`, `/remind`

### AI Orchestration

The AI orchestrator classifies user intent and routes to appropriate tools:
- `ServiceSearch` - Find relevant government services
- `StartWizard` - Begin guided process
- `ShowDocuments` - Retrieve relevant forms and guides
- `QuickAttachTracker` - Monitor application status
- `AddReminder` - Set deadline reminders
- `CreateTask` - Initialize task tracking

### Real-Time Updates

SignalR provides instant updates for:
- New chat messages
- Task status changes
- Tracker updates
- Reminder notifications

### Context Panel

The right-side panel displays:
- Current task progress with percentage
- Task status and deadline
- Tracker status with live updates
- Connection status indicator

## Usage Examples

### Searching for Services

```
User: "How do I renew my vehicle fitness?"
Assistant: [Returns Vehicle Fitness & Registration service chip]
```

### Starting a Process

```
User: "Help me with NHT benefits"
Assistant: [Initiates wizard with guided questions]
```

### Checking Status

```
User: "Track my application"
Assistant: [Displays tracker status with updates]
```

### Using Commands

```
/new         - Start a new service search
/docs nht    - Search for NHT documents
/track       - Quick attach tracker
/remind 1d   - Set reminder in 1 day
```

## API Endpoints

### Chat

- `POST /api/chat` - Send a chat message
  ```json
  {
    "userId": "user-123",
    "message": "How do I renew fitness?",
    "currentTaskId": "task-guid"
  }
  ```

- `GET /api/chat/history/{userId}` - Get conversation history
- `GET /api/chat/task/{userId}` - Get current user task

### SignalR Hub

- `/chathub` - SignalR connection endpoint
  - Events: `ReceiveMessage`, `TaskUpdated`, `TrackerUpdated`, `ReminderDue`

## Configuration

### API Configuration (appsettings.json)

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore.SignalR": "Debug"
    }
  },
  "AllowedHosts": "*"
}
```

### CORS Settings

The API is configured to allow requests from:
- `https://localhost:5001`
- `http://localhost:5000`
- `https://localhost:7001`
- `http://localhost:5002`

## Development

### Adding New Services

1. Add service definition to `ServiceCatalogService.cs`
2. Update AI patterns in `AIOrchestrationService.cs`
3. Add corresponding wizard templates

### Adding New Tools

1. Add tool name to `AvailableTools` class
2. Implement handler in `ChatOrchestrator.cs`
3. Update AI classification logic

### Extending Rich Content

1. Add new type to `RichContentType` enum
2. Implement rendering logic in `Chat.razor`
3. Create corresponding CSS styles

## Testing

### Manual Testing

1. Start both API and app
2. Open browser to `https://localhost:5001`
3. Try example queries:
   - "Tell me about vehicle fitness"
   - "I need help with NHT loans"
   - "Show me tax documents"

### Mock Data

The application uses in-memory mock services for:
- Service catalog (3 seeded services)
- Tracker updates (simulated status transitions)
- Document search (mock document links)

## Troubleshooting

### SignalR Connection Issues

If the connection status shows "Disconnected":
1. Verify API is running at `https://localhost:7001`
2. Check CORS configuration
3. Review browser console for connection errors

### Build Errors

If you encounter build errors:
1. Ensure .NET 9.0 SDK is installed: `dotnet --version`
2. Clean and rebuild: `dotnet clean && dotnet build`
3. Restore packages: `dotnet restore`

## Future Enhancements

- [ ] Azure OpenAI integration for production
- [ ] Persistent storage (SQL Server/Cosmos DB)
- [ ] User authentication and authorization
- [ ] Document OCR and parsing
- [ ] SMS/Email reminder notifications
- [ ] Mobile app (Blazor Hybrid)
- [ ] Advanced analytics dashboard
- [ ] Multi-language support

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Submit a pull request

## License

This project is licensed under the MIT License.

## Contact

For questions or support, please open an issue on GitHub.

---

Built with ❤️ for the Government of Jamaica Digital Transformation Initiative
