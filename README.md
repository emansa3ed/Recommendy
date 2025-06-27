# Recommendy 🎓

A comprehensive AI-powered platform that connects students with internship and scholarship opportunities. Built with .NET 8, Entity Framework Core, and SignalR for real-time communication, featuring advanced AI recommendations, organization verification, and premium subscription services.

## 🌟 Core Features

### For Students
- **AI-Powered Recommendations**: Get personalized internship and scholarship recommendations based on your skills and profile using Ollama LLM (see [Recommendation Endpoints & Logic](#recommendation-endpoints--logic))
- **Resume Parser**: Upload your resume to automatically extract and match your skills with opportunities
- **Save Opportunities**: Bookmark and manage your favorite internships and scholarships
- **Real-time Notifications**: Receive instant updates about new opportunities and application status
- **Profile Management**: Complete profile with skills, interests, and preferences
- **Chat System**: Communicate with companies and universities directly
- **AI Chatbot Assistant**: Get instant help and guidance through our intelligent chatbot (Students only)
- **Interesting Profile System**: Discover and connect with companies based on shared interests and skills
- **Premium Features**: Access advanced features with subscription-based premium membership

### For Companies
- **Internship Management**: Create, edit, and manage internship opportunities
- **Real-time Communication**: Chat with potential interns
- **Communication Hub**: Connect with students and provide guidance
- **Organization Verification**: Get verified by admins to build trust
- **Analytics Dashboard**: Track application statistics and engagement

### For Universities
- **Scholarship Management**: Create and manage scholarship programs
- **Communication Hub**: Connect with students and provide guidance
- **Organization Verification**: Get verified by admins to build trust
- **Analytics Dashboard**: Track application statistics and engagement

### For Administrators
- **Comprehensive Dashboard**: Monitor platform statistics and user activity
- **Content Moderation**: Review and approve opportunities
- **User Management**: Manage users, roles, and permissions
- **Organization Verification**: Verify companies and universities
- **System Analytics**: Track platform performance and usage
- **Report Management**: Handle user reports and content violations

## 🌟 Architecture

### Architecture Pattern
This project follows the **Onion Architecture** pattern, which provides a clean separation of concerns and dependency inversion. The architecture consists of multiple layers:

- **Core Domain Layer**: Contains business entities and domain logic
- **Application Layer**: Contains use cases and business rules
- **Infrastructure Layer**: Contains external concerns like databases, APIs, and services
- **Presentation Layer**: Contains controllers and API endpoints

### Technology Stack
- **Backend**: .NET 8 Web API
- **Database**: SQL Server with Entity Framework Core
- **Authentication**: JWT Bearer Tokens with ASP.NET Core Identity
- **Real-time Communication**: SignalR Hubs
- **AI Integration**: Ollama LLM for recommendations and chatbot
- **Email Service**: SMTP with HTML templates using MailKit
- **File Storage**: Local file system for images and documents
- **Payment Processing**: Stripe integration for premium features
- **Caching**: In-memory caching for performance optimization
- **Document Processing**: Resume parsing with iTextSharp and OpenXML

### Project Structure
```
Recommendy/
├── Contracts/          # Repository interfaces (Core Domain)
├── Entities/           # Domain models and entities (Core Domain)
├── Presentation/       # API Controllers (Presentation Layer)
├── Repository/         # Data access layer (Infrastructure Layer)
├── Service/            # Business logic layer (Application Layer)
├── Service.Contracts/  # Service interfaces (Application Layer)
├── Shared/            # DTOs and shared models (Application Layer)
└── Recommendy/        # Main application (Infrastructure Layer)
```

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server (LocalDB or full instance)
- Visual Studio 2022 or VS Code
- SMTP email service (Gmail recommended)

### Environment Setup

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd Recommendy
   ```

2. **Set up environment variables**
   
   Open Command Prompt as Administrator and run:
   ```cmd
   setx SecretKey "your-secret-key" /M
   setx EmailPassword "your-email-password" /M
   setx GeminiKey "your-gemini-api-key" /M
   setx StripePublishableKey "your-stripe-publishable-key" /M
   setx StripeSecretKey "your-stripe-secret-key" /M
   setx PriceId "your-stripe-price-id" /M
   setx GOOGLE_CLIENT_ID "your-google-client-id" /M
   ```

3. **Configure database connection**
   
   Update the connection string in `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "sqlConnection": "Server=(localdb)\\mssqllocaldb;Database=RecommendyDB;Trusted_Connection=true;MultipleActiveResultSets=true"
     }
   }
   ```

4. **Run database migrations**
   ```bash
   cd Recommendy
   dotnet ef database update
   ```

5. **Build and run the application**
   ```bash
   dotnet build
   dotnet run
   ```

The API will be available at `https://localhost:7001` (or your configured port).

## 📚 API Documentation

### Authentication Endpoints
- `POST /api/Authentication/Register` - User registration with role selection
- `POST /api/Authentication/login` - User login
- `POST /api/Authentication/google-register` - Google OAuth registration
- `POST /api/Authentication/google-login` - Google OAuth login
- `POST /api/Authentication/ForgotPassword` - Password reset request
- `POST /api/Authentication/ResetPassword` - Password reset
- `POST /api/token/refresh` - Refresh JWT token

### Student Endpoints
- `GET /api/Student/{StudentId}/saved-scholarships` - Get saved scholarships
- `GET /api/Student/{StudentId}/saved-internships` - Get saved internships
- `POST /api/Student/{StudentId}/profile-suggestions` - Get profile suggestions

### Opportunity Endpoints
- `GET /api/Opportunities/RecommendedScholarships` - Get AI-recommended scholarships
- `GET /api/Opportunities/RecommendedInternships` - Get AI-recommended internships
- `GET /api/Opportunities/Scholarships/all` - Get all scholarships
- `GET /api/Opportunities/Internships/all` - Get all internships
- `POST /api/Opportunities/SaveOpportunity` - Save an opportunity
- `DELETE /api/Opportunities/DeleteOpportunity` - Remove saved opportunity

### Company Endpoints
- `POST /api/Companies/{CompanyId}/internships` - Create internship
- `PUT /api/Companies/{CompanyId}/internships/{id}` - Update internship
- `DELETE /api/Companies/{CompanyId}/internships/{id}` - Delete internship
- `GET /api/Companies/{CompanyId}/internships` - Get company internships
- `GET /api/Companies/profile/{id}` - Get company profile
- `PATCH /api/Companies/edit-profile/{id}` - Edit company profile

### University Endpoints
- `POST /api/Universities/{UniversityId}/scholarships` - Create scholarship
- `PUT /api/Universities/{UniversityId}/scholarships/{id}` - Update scholarship
- `DELETE /api/Universities/{UniversityId}/scholarships/{id}` - Delete scholarship
- `GET /api/Universities/{UniversityId}/scholarships` - Get university scholarships
- `GET /api/Universities/profile/{id}` - Get university profile
- `PATCH /api/Universities/edit-profile/{id}` - Edit university profile

### Chat Endpoints
- `GET /api/Chat/users` - Get chat users
- `POST /api/Chat` - Create new chat
- `GET /api/Messages/{chatId}` - Get chat messages
- `POST /api/Messages` - Send message

### Chatbot Endpoints
- `POST /api/ChatBot/generate` - Send message to AI chatbot
- `GET /api/ChatBot/history` - Get chat history
- `POST /api/ChatBot/feedback` - Submit chatbot feedback

### Profile & Interest Endpoints
- `GET /api/ProfileSuggestion/interesting-profiles` - Get interesting profiles based on interests
- `POST /api/ProfileSuggestion/update-interests` - Update user interests
- `GET /api/ProfileSuggestion/matched-profiles` - Get profiles with matching interests
- `POST /api/ProfileSuggestion/connect` - Connect with another user

### Skill Management Endpoints
- `GET /api/Skill` - Get user skills
- `POST /api/Skill` - Add skills
- `POST /api/Skill/UploadResume` - Upload resume to extract skills
- `DELETE /api/Skill/{skill}` - Remove skill

### Feedback & Rating Endpoints
- `GET /api/Organization/{OrganizationID}/Posts/{PostId}/FeedBack` - Get feedback for post
- `POST /api/Organization/{OrganizationID}/Posts/{PostId}/FeedBack` - Create feedback
- `PUT /api/Organization/{OrganizationID}/Posts/{PostId}/FeedBack/{feedbackId}` - Update feedback
- `DELETE /api/Organization/{OrganizationID}/Posts/{PostId}/FeedBack/{feedbackId}` - Delete feedback

### Report Endpoints
- `POST /api/Report/Students/{StudentId}/Posts/{PostId}` - Report a post
- `GET /api/Report/all` - Get all reports (Admin only)
- `PUT /api/Report/{reportId}/status` - Update report status

### Notification Endpoints
- `GET /api/Receiver/{ReceiverID}/Notification` - Get user notifications
- `GET /api/Receiver/{ReceiverID}/Notification/{NotificationId}` - Get specific notification

### Subscription Endpoints
- `POST /api/Subscription` - Create subscription
- `GET /api/Subscription/is-premium` - Check premium status
- `DELETE /api/Subscription/Cancel` - Cancel subscription
- `POST /api/Subscription/SubscriptionRenewed` - Handle subscription renewal
- `POST /api/Subscription/PaymentFailed` - Handle payment failure

### Admin Endpoints
- `GET /api/Admin/dashboard` - Get admin dashboard statistics
- `GET /api/Admin/users` - Get all users
- `PUT /api/Admin/users/{userId}/ban` - Ban/unban user
- `GET /api/Admin/reports` - Get system reports
- `GET /api/Admin/companies/unverified` - Get unverified companies
- `PATCH /api/Admin/companies/{id}/verify` - Verify company
- `GET /api/Admin/universities/unverified` - Get unverified universities
- `PATCH /api/Admin/universities/{id}/verify` - Verify university

### Organization Profile Endpoints
- `GET /api/profile/{id}` - Get organization profile

### Country Endpoints
- `GET /api/Countries` - Get all countries

## 🔧 Configuration

### JWT Settings
Configure JWT settings in `appsettings.json`:
```json
{
  "JWT": {
    "validIssuer": "https://localhost:7001",
    "validAudience": "https://localhost:7001",
    "expires": "60"
  }
}
```

### Email Configuration
Update email settings in `appsettings.json`:
```json
{
  "EmailSettings": {
    "Host": "smtp.gmail.com",
    "Port": 587,
    "Username": "your-email@gmail.com",
    "EnableSsl": true
  }
}
```

### CORS Configuration
Configure CORS in `ServiceExtensions.cs` for your frontend application.

## 🤖 AI Integration

The platform uses multiple AI services for different purposes:
**Ollama LLM**: For intelligent chatbot responses and conversation handling
- **Ollama LLM**: For skill-based recommendations and content analysis
- **Regex Pattern Matching**: For question classification and response routing
- **Smart filtering**: Provide intelligent search and filtering options

## 💬 Chatbot System

### Features
- **Student-Only Access**: Exclusive chatbot assistance for students
- **Ollama Local Models**: AI-powered responses using Ollama running on localhost:11434
- **Regex-Based Question Classification**: Intelligent question filtering using regex patterns
- **Career-Focused Responses**: Specialized for career advice, internships, and education
- **Dynamic Context Integration**: Incorporates student skills and available opportunities
- **Streaming Support**: Real-time streaming responses for better user experience
- **Prompt Templates**: Structured prompts for consistent and concise responses
- **Error Handling**: Graceful error handling with fallback responses

## 👥 Profile Suggestion System

### Features
- **Skill-Based Matching**: Match student skills with companies and universities using intelligent skill expansion
- **Ontology-Driven Matching**: Uses a comprehensive skill ontology to find related skills and synonyms
- **Content Analysis**: Analyzes company bios, internship descriptions, and position requirements
- **University Matching**: Matches student skills with scholarship requirements and university programs
- **Scoring Algorithm**: Calculates match scores based on skill compatibility
- **Caching System**: Optimized performance with 30-minute cache for profile suggestions
- **Top 5 Recommendations**: Returns the best 5 matching companies and universities

## 🏢 Organization Verification System

### Features
- **Verification Process**: Companies and universities must be verified by admins
- **Trust Building**: Verified organizations display verification badges
- **Admin Review**: Comprehensive review process for organization legitimacy
- **Verification Notes**: Admins can add notes during verification process
- **Email Notifications**: Automatic notifications for verification status changes

## 💳 Premium Subscription System

### Features
- **Stripe Integration**: Secure payment processing
- **Subscription Management**: Easy subscription creation and cancellation
- **Premium Role**: Special "PremiumUser" role with enhanced features
- **Automatic Renewal**: Seamless subscription renewal process
- **Payment Failure Handling**: Graceful handling of failed payments
- **Email Notifications**: Automatic emails for subscription events

## 📊 Real-time Features

SignalR hubs for real-time communication:
- **NotificationHub**: Real-time notifications
- **MessageHub**: Live chat functionality
- **FeedbackHub**: Real-time feedback updates

## 🔒 Security Features

- JWT-based authentication with refresh tokens
- Role-based authorization (Student, Company, University, Admin, PremiumUser)
- Email confirmation for new accounts
- Password reset functionality with secure codes
- Input validation and sanitization
- Rate limiting for API endpoints
- Chatbot conversation security and privacy
- Organization verification for trust and security
- Secure file upload handling
- CORS configuration for frontend security

## 📧 Email System

### Features
- **SMTP Integration**: Using MailKit for reliable email delivery
- **HTML Templates**: Professional email templates
- **Email Confirmation**: Account verification emails
- **Password Reset**: Secure password reset emails
- **Notification Emails**: System notifications and updates
- **Subscription Emails**: Payment and subscription notifications
- **Organization Verification**: Verification status emails

## 🧪 Testing

Run unit tests:
```bash
cd RecommendyUnitTests
dotnet test
```

## Recommendation Endpoints & Logic

Recommendy provides personalized recommendations for both internships and scholarships to students, leveraging AI (Ollama LLM) and a user's skill profile. The recommendation logic is implemented in the following endpoints:

- **GET `/api/Opportunities/RecommendedInternships`**
- **GET `/api/Opportunities/RecommendedScholarships`**

### How the Recommendation Logic Works

1. **Skill Extraction:**  
   When a student requests recommendations, the system retrieves the student's skills from their profile.

2. **Data Preparation:**  
   The system fetches a list of available internships or scholarships (up to 50 at a time), including their names, descriptions, and IDs.

3. **Chunking:**  
   To handle large data, the list is split into manageable JSON chunks.

4. **Prompt Engineering & AI Matching:**  
   For each chunk, a prompt is constructed and sent to the Ollama LLM. The prompt asks the AI to select the IDs of opportunities that best match the student's skills, based on the name and description fields.

5. **ID Aggregation:**  
   The AI returns a comma-separated list of matching IDs for each chunk. All IDs are aggregated, deduplicated, and combined.

6. **Caching:**  
   The resulting list of recommended IDs is cached (with a sliding expiration) to optimize performance for repeated or similar queries.

7. **Filtering & Pagination:**  
   The system queries the database for the full details of the recommended opportunities, applying any additional filters (e.g., paid/unpaid, degree, funding) and paginates the results.

### Technologies Used

- **Ollama LLM** for semantic matching and recommendations
- **In-memory caching** for performance
- **.NET 8 Web API** for endpoints
- **Entity Framework** for data access

### Key Points

- Recommendations are **personalized** and **AI-driven**.
- The system is **scalable** (handles large data via chunking and caching).
- The logic is **modular** and can be extended to other types of opportunities.



## Entity Relationship Diagram (ERD)

![image](https://github.com/user-attachments/assets/60659300-fe01-4cad-9996-e568d17cf313)



---

**Built with ❤️ for students, companies, and universities**


