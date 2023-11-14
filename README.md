***DOC\_174\_REV\_C\_ASSESMENT AND IV FRONT SHEET\_INDIVIDUAL CRITERIA\_CLASSTER.docx** ![](readme_resources/Aspose.Words.4c3f15a1-6727-4043-b92f-43e85c368d02.001.png)*

**ASSESSMENT AND INTERNAL VERIFICATION FRONT SHEET (Individual Criteria) (Note : This version is to be used for an assignment brief issued to students via Classter)** 



|**Course  Title** |Enterprise Programming |**Lecturer Name & Surname** |Ryan Attard |||||
| :- | - | -: | - | :- | :- | :- | :- |
|**Unit Number & Title** |[ITSFT-506-1616 - Enterprise Programming ](https://moodle.mcast.edu.mt/course/view.php?id=3560)|||||||
|**Assignment Number, Title / Type** |1 - Building An Enterprise Application using a clean architecture |||||||
|**Date Set** |6/11/2023 |**Deadline Date**  |8/1/2024 |||||
|**Student Name** ||**ID Number** ||**Class / Group** ||||



||Assessment Criteria |Maximum Mark |
| :- | - | - |
|KU1.1* |Describe what is meant by software architecture and the role that it offers while expressing it in practice* |5 |
|KU1.2* |Interpret what is meant by refactoring* |5 |
|KU2.1* |Show correct use of software design pattern* |5 |
|KU3.4 |Find and select a perspective of versioning, configuration and scalability of the solution |5 |
|AA2.3 |Select and implement appropriate design patterns to a solution being implemented |7 |
|AA3.2 |Choose and develop the correct delivery model |7 |
|AA4.3 |Use and draw  practical application to upload application content to cloud services |7 |
|SE1.3 |Construct and Ascertain that enterprise standards fit within an enterprise solution |10 |
|SE3.3 |Identify and design security in data integrity features |10 |
||**Total Mark:** |61 |

**Notes to Students:**

- This assignment brief has been approved and released by the Internal Verifier through Classter.
- Assessment marks and feedback by the lecturer will be available online via Classter [(Http://mcast.classter.com)](http://mcast.classter.com/) following release by the Internal Verifier  
- Students submitting their assignment on VLE will be requested to confirm online the following statements: 

` `**Student’s declaration prior to handing-in of assignment**  

- I certify that the work submitted for this assignment is my own and that I have read and understood the respective Plagiarism Policy 

` `**Student’s declaration on assessment special arrangements** 

- I certify that adequate support was given to me during the assignment through the Institute and/or the Inclusive Education Unit. 
- I declare that I refused the special support offered by the Institute. 

**Assignment Guidelines** 

Read  the  following  instructions  carefully  before  you  start  the  assignment.  If  you  do  not understand any of them, ask your invigilator. 

- This assignment is a HOME assignment. 
- Fill in and print/scan the assignment sheet. 
- Copying and Pasting code from any source is **Strictly Prohibited** and will be penalised according to disciplinary procedures – code has to be yours.  An interview will follow and random questions on YOUR work will be asked.  Any signs that show that the work has been copied or AI-generated and no effort was put to understand what’s happening will result in no acceptance of the work submitted. 
- Deadline: See front page 
- This assignment has a total of 61 marks.  Follow the rubric at the back for detailed assessment guidelines. 
- Submission must be done by inputting your Git repository link or VLE. 

***Home Assignment: Building a simple Airline Ticket Reservation Website*** 

Note:  The assignment is composed of a number of Tasks each of which is linked to a number of criteria which ultimately all contribute to one final product.  It is divided in two parts – Part 1 and Part 2.  Part 1 is basic stuff which should be enough for an anonymous person to make use of the basic functionalities, while Part 2 will require authentication to provide more services. 

Part 1 

1. (KU3.4) – *Find and select a perspective of versioning, configuration and scalability of the solution [5]* 

Application  has  to  be  configured  and  stored  on  a  git  repository,  keeping  all  versions  it  went  throughout development till deployment.  Git repository must show evidence that you have been continuously updating the solution with different versions for at least 4 weeks. Hence evidence of a difference of 4 weeks between the start date committing the architecture described in Ku1.1 and the final submission must be shown. 

2. (KU1.1) – *Describe what is meant by enterprise software architecture and the role that it offers in practice [5]* 

Create a .NET Core Web Application which consists of at least the following projects: 

1. **Domain** contains all the models you will be using in your application 
1. **Data** contains all the repository and context classes you will make use of to interact directly with your database 
1. **Application** (optional)** contains your services and view-models (optional – because this can be done in the Presentation layer/web application) 
1. **Presentation  –**  is  the  web  application  with  the  respective  Controllers  and  Views  and ViewModels 

You will need at least the following classes. It is your task to find out where you are going to create them: 

- AirlineDbContext.cs 
- Flight.cs – Id,  Rows, Columns, DepartureDate, ArrivalDate, CountryFrom, CountryTo, WholesalePrice, CommissionRate 
- Ticket.cs – Id, Row, Column, FlightIdFK, Passport, PricePaid, Cancelled 
- TicketDBRepository.cs – at least methods: Book(…), Cancel(…), GetTickets() 
- FlightDbRepository.cs – GetFlight(), GetFlights() 

You will get 1 mark for each of these classes created in the correct tier. 

3. (KU1.2) - *Interpret what is meant by refactoring* *[5]* 
1) The TicketDBRepository.cs class must contain the following methods.: 
1. Book () – *a method that allows any client to book a ticket.  Double booking a seat is not allowed [1].* 
2. Cancel() – *a method which allows the booked ticket to be cancelled and not deleted. Once cancelled someone else can book the released seat again [1]* 
2. GetTickets – *a method which returns all the tickets for a flight selected [1]* 
2) The *FlightDbRepository*.cs class must contain the following methods: 
4. *GetFlight() – get the flight info by id [1]* 
4. *GetFlights() – returns all flights in db [1]* 
4. (KU2.1) – *Show correct use of a software design pattern [5]* 

In the following task you will implement the Façade design pattern by: 

Develop the BusinessLogic/Webapplication tier with a controller class called TicketsController.cs within the website 

1) Method (and View) which returns and shows on page a list of Flights that one can choose with RETAIL prices displayed;  Requirements [1.5]: 
   1. Fully booked flights should not be allowed to be selected but still shown on screen ;  
   1. If departure date is in the past, flight should not be returned and not even be displayed  
1) Method (and View) which allows the user to book a flight after entering the requested details to book a ticket. [2]  However you must respect the following requirements 
   1. Check that flight ticket selected has not be booked yet or was cancelled; 
   1. Check that flight departure date is in the future; 
   1. PricePaid is filled automatically after calculating commission on WholesalePrice; 
1) Method (and View) which returns a list of Tickets (i.e. use GetTickets from Repository) which then returns a history list of tickets purchased by the logged in client (See Se3.3 for authentication) [1.5] 
5. (AA4.3) - *Use and draw practical application to upload application content to cloud services [7]* 

Modify where appropriate to ask the user to upload a photo of his/her passport while booking the ticket.  Passport image should be stored in a folder uniquely renamed but referenced within the Ticket model.  Paths should not be entirely hard-coded i.e. if I host the website on a different drive, it should continue working seamlessly. [3.5] 

Create an AdminController.cs, where it allows the admin to select from a list of flights, get a list of tickets, select a ticket and will be able to view info about the ticket including the passport image uploaded. [3.5] 

6. (AA3.2) - *Choose and develop the correct delivery model [7]* 

Website should be deployed on the cloud or any hosting provider [1] of your choice [e.g.[ Asp Hosting (Trial 60 Days free)\]](https://www.myasp.net/index?r=100572350) and configured to run properly [2 for Booking of A Ticket including passport upload, 1 for Login, 3 for the selection of seat working properly on the remote server]. 

Part 2 

7. (AA2.3) – *Select and implement appropriate design patterns to a solution being implemented [7]* 

Find a solution where the above calls can be implemented NOT using a database but a JSON file.  Therefore Implement TicketFileRepository.cs and  

- Book() [2] 
- Cancel() [2] 
- GetTickets() [2] 

Should all show correct code implementation (check the requirements in KU2.1) if the data layer was based on a file system and not on a database. 

- Implement an interface ITicketRepository.cs from which TicketFileRepository and TicketDBRepository should inherit and change where appropriate any constructor injection [1] 
- It  is  your  responsibility  to  modify  the  startup.cs  /  program.cs  to  inject  the  TicketFileRepository accordingly and check that it works  
8. (SE1.3) *- Construct and ascertain that enterprise standards fit within an enterprise solution [10]* 
- Find a way how a Flight seats configuration (denoted by Rows and Columns; i.e. NOT  a dropdown, ideally a table showing rows/columns instead of seats) can be visually shown on screen dynamically to the end-user whereby these can be clicked upon/selected and the user is left with the rest of the details to be filled and everything is submitted to the backend as described in KU2.1. This should work without issues;[7] 
- ViewModels should be used when passing data to and from the pages.  Hence 
  - When retrieving and passing flight info, a view model should be used to hide wholesale prices and commission but display the retail price [1] 
- Dependency Injection 
- Must show evidence of using Constructor Injection [1] 
- Must show evidence of using Method Injection [1] 
9. (SE3.3) - *Identify and design basic security in data integrity features [10]* 
1) In Ku2.1 an end user is asked to input hers/his passport no.  Activate authentication by inheriting from IdentityDbContext, customize the registration process so that it accepts the passport no as well and make the necessary changes so that when the user is about to book a flight he/she is given the option to register/login and the details (e.g. the passport no) is filled in automatically.  The user can still choose not to register or login and still be able to book a ticket. [3]* 
1) In Ku2.1 you are expected to show past purchases for the logged in user. Marks will be given if a check whether the user is logged in or not before actually displaying past tickets is applied.  Feature has to work by getting a list, filter that list, show past purchases and user can see the details of each purchase [2]* 
1) Exceptions should be mitigated by showing a CUSTOM simple message at the top of any page (hint: use the Layout page) [2].  If during the interview something fails and no exception handling is present – you will lose 2 marks;* 
1) Make use of the [Remote] or [Custom] attribute so that the overbooking of seats never happens and is applied within the ViewModel [3]* 



|Criterion |Description |Met/Not Met |
| - | - | - |
|KU1.1|Classes indicated are created in the right project/tier (5 classes) ||
|KU1.2|<p>TextDbRepository </p><p>- Book  - double booking is not allowed[1] </p><p>- Cancel [1] </p><p>- GetTickets [1] </p><p>FlightDbRepository </p><p>- GetFlight [1] </p><p>- GetFlights[1] </p>||
|KU2.1|<p>- Listing of Flights [1.5] </p><p>- Indication of fully booked flights </p><p>- Hidden past flights </p><p>- Book flight [2] </p><p>- No double booking </p><p>- Price paid computed automatically </p><p>- Flight in the future </p><p>- GetTickets for logged in user [1.5] </p>||
|KU3.4|<p>Git repository showing a difference between the start date (committing ku1.1) and the last date (when pushed for the final submission) of 4 weeks; note to earn these 3 marks all KU1.1 has to be present NOT a missing project [3 marks] </p><p>Use of repository [2 marks] </p>||
|AA2.3|<p>Advanced feature (accessing a file): </p><p>￿  Implement a TicketFileRepository with </p><p>- Book [2] </p><p>- Cancel [2] </p><p>- GetTickets 2] </p><p>- ITicketRepository [1] </p>||
|AA3.2|<p>Deployment done and website works </p><p>- Deployment of the website[1] </p><p>- Add Ticket together with passport upload [2] </p><p>- Login [1] </p><p>- Dynamic display of the seating and selection is working [3] </p>||
|AA4.3|<p>- Passport Upload (dynamic path) [3.5] </p><p>- Admin – listing of flights [3.5] </p>||
|SE1.3|<p>- Show seating and clickable [7] </p><p>- ViewModels are used for flight info [1] </p><p>- Constructor Injection [1] </p><p>- Method Injection [1] </p>||
|SE3.3|<p>- authentication and registration using IdentityDbContext base class [1 – for automatic filling of details, 2 – for customization process]* </p><p>- Authentication check before showing purchases + showing details of tickets [2]* </p><p>- Exception messages shown on each page (layout page is used) [2] </p><p>- No booking (ticket) details are allowed to be left blank from the model [3] </p>||

***MCAST Controlled and approved document                                                              Unauthorised copying or communication strictly prohibited*** 

6 
