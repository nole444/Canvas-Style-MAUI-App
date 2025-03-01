using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Canvas.Helpers
{
    internal class CourseHelper
    {
        private CourseService courseService;
        private StudentService studentService;

        public CourseHelper()
        {
            studentService = StudentService.Current;
            courseService = CourseService.Current;
        }

        public void CreateSubmission(Course c, Person student, Assignment assignment)
        {
            if(assignment == null || student == null)
            {
                return;
            }
            Console.WriteLine("What is the content of the submission?");
            var content = Console.ReadLine();
            c.Submissions.Add(new Submission
            {
                Student = student,
                Assignment = assignment,
                Content = content ?? string.Empty
            });
        }
      /*  public void GradeSubmission()
        {
            Console.WriteLine("Enter the course code to add the assignment to:");
            courseService.Courses.ForEach(Console.WriteLine);
            var choice = Console.ReadLine();

            var courseSelected = courseService.Courses.FirstOrDefault(s => s.Code.Equals(choice, StringComparison.InvariantCultureIgnoreCase));
            if(courseSelected != null)
            {
                courseSelected.Submissions.ForEach(Console.WriteLine);
                var chosenId = int.Parse(Console.ReadLine() ?? "0");

                Console.WriteLine("Enter grade:");
                courseSelected.Submissions.FirstOrDefault(s => s.Id == chosenId).Grade = decimal.Parse(Console.ReadLine() ?? string.Empty);
            }
        }
        */
        private Assignment CreateAssignment()
        {
            Console.WriteLine("Name:");

            var assignmentName = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Description:");
            var assignmentDescription = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Total Points:");
            var assignmentPoints = decimal.Parse(Console.ReadLine() ?? "100");

            Console.WriteLine("Due-Date:");
            var assignmentDueDate = DateTime.Parse(Console.ReadLine() ?? "01-01-1900");

            return new Assignment
            {
                Name = assignmentName,
                Description = assignmentDescription,
                TotalPointsAvailable = assignmentPoints,
                DueDate = assignmentDueDate
            };

          /*  var assignment = new Assignment();

            Console.WriteLine("Enter assignment name:");
            assignment.Name = Console.ReadLine();

            Console.WriteLine("Enter assignment description:");
            assignment.Description = Console.ReadLine();

            Console.WriteLine("Enter total points available for the assignment:");
            if (decimal.TryParse(Console.ReadLine(), out decimal totalPoints))
            {
                assignment.TotalPointsAvailable = totalPoints;
            }
            else
            {
                Console.WriteLine("Invalid points format.");
                return null;
            }

            Console.WriteLine("Enter assignment due date (yyyy-mm-dd):");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime dueDate))
            {
                assignment.DueDate = dueDate;
            }
            else
            {
                Console.WriteLine("Invalid date format.");
                return null;
            }

            return assignment;*/
        }
        private void SetUpAssignments(Course c)
        {
            Console.WriteLine("Do you want to add assignments (Y/N)");
            var choice = Console.ReadLine() ?? "N";
            bool continueAdding;

            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
               {
                continueAdding = true;
                while (continueAdding)
                 {
                   CreateAssignmentWithGroup(c);

                    Console.WriteLine("Add more? (Y/N)");

                    choice = Console.ReadLine() ?? "N";
                    if (choice.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                    {
                        continueAdding = false;
                    }
                 }
              }
        }
        

       /* public void AddAssignment()
        {
            Console.WriteLine("Enter the course code you would like to add the assignment to:");
            courseService.Courses.ForEach(Console.WriteLine);
            var choice = Console.ReadLine();

            var courseSelected = courseService.Courses.FirstOrDefault(s => s.Code.Equals(choice, StringComparison.InvariantCultureIgnoreCase));
            if(courseSelected != null)
            {
                CreateAssignmentWithGroup(courseSelected);
            }
        }*/
      /*  public void AddSubmission()
        {
            Console.WriteLine("Enter the course code you would like to add the assignment to:");
            courseService.Courses.ForEach(Console.WriteLine);
            var choice = Console.ReadLine();

            var courseSelected = courseService.Courses.FirstOrDefault(s => s.Code.Equals(choice, StringComparison.InvariantCultureIgnoreCase));
            if (courseSelected != null)
            {
                Console.WriteLine("Enter the id for the student:");
                courseSelected.Roster.ForEach(Console.WriteLine);
                var studentId = int.Parse(Console.ReadLine() ?? "0");
                var chosenStudent = courseSelected.Roster.FirstOrDefault(s => s.Id == studentId);

                Console.WriteLine("Enter the assignment Id:");

                courseSelected.Assignments.ToList().ForEach(Console.WriteLine);

                var assignmentId = int.Parse(Console.ReadLine() ?? "0");
                var chosenAssignment = courseSelected.Assignments.FirstOrDefault(a => a.Id ==  assignmentId);
              

                CreateSubmission(courseSelected, chosenStudent, chosenAssignment);
            }
        }*/

        private void CreateAssignmentWithGroup(Course courseSelected)
        {
            courseService.Courses.ForEach(Console.WriteLine);
            if (!courseSelected.AssignmentGroups.Any())
            {
                Console.WriteLine("Add a new group");
                courseSelected.AssignmentGroups.ForEach(Console.WriteLine);

                var selectedStr = Console.ReadLine() ?? string.Empty;
                var selectionInt = int.Parse(selectedStr);

                if(selectionInt == 0)
                {
                    var newGroup = new AssignmentGroup();
                    Console.WriteLine("Group Name:");

                    newGroup.Name = Console.ReadLine() ?? string.Empty;
                    Console.WriteLine("Group Weight:");
                    newGroup.Weight = decimal.Parse(Console.ReadLine() ?? "1");


                    newGroup.Assignments.Add(CreateAssignment());
                    courseSelected.AssignmentGroups.Add(newGroup);
                }
                else if(selectionInt != 0)
                {
                    var groupChoice = courseSelected.AssignmentGroups.FirstOrDefault(g => g.Id == selectionInt);
                    if(groupChoice != null)
                    {
                        groupChoice.Assignments.Add(CreateAssignment());
                    }
                }
                else
                {
                    var newGroup = new AssignmentGroup();
                    Console.WriteLine("Name:");

                    newGroup.Name = Console.ReadLine() ?? string.Empty;
                    Console.WriteLine("Weight:");
                    newGroup.Weight = decimal.Parse(Console.ReadLine() ?? "1");
                    newGroup.Assignments.Add(CreateAssignment());
                    courseSelected.AssignmentGroups.Add(newGroup);
                }
            }
        }
        private Module CreateModule(Course c)
        {
            Console.WriteLine("Name:");
            var name = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Description:");
            var description = Console.ReadLine() ?? string.Empty;

            var module = new Module
            {
                Name = name,
                Description = description
            };

            Console.WriteLine("Would you like to add content? (Y/N)");
            var choice = Console.ReadLine() ?? "N";

            while(choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("What content would you like to add?");
                Console.WriteLine("1. Assignment");
                Console.WriteLine("2. File");
                Console.WriteLine("3. Page");

                var contentChosen = int.Parse(Console.ReadLine() ?? "0");

                switch(contentChosen)
                {
                    case 1:
                        var newContent = CreateAssignmentItem(c);
                       if (newContent == null)
                        {
                            module.Content.Add(newContent);
                        }
                        break;
                    case 2:
                        var newFileContent = CreateFileItem(c);
                        if (newFileContent == null)
                        {
                            module.Content.Add(newFileContent);
                        }
                        break;
                    case 3:
                        var newPageContent = CreatePageItem(c);
                        if (newPageContent == null)
                        {
                            module.Content.Add(newPageContent);
                        }
                        break;
                    default:
                        break;

                }
                Console.WriteLine("Would you like to add more content? (Y/N)");
                 choice = Console.ReadLine() ?? "N";
            }
            return module;
        }

        private FileItem? CreateFileItem(Course c)
        {
            Console.WriteLine("Name:");
            var name = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Description:");
            var description = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter a file path to follow:");
          var filePath = Console.ReadLine();

            return new FileItem { Path = filePath, Name = name, Description = description };
        }

        private PageItem? CreatePageItem(Course c)
        {
            Console.WriteLine("Name:");
            var name = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Description:");
            var description = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter page content:");
            var body = Console.ReadLine();

            return new PageItem { HTMLBody = body, Name = name, Description = description };
        }
        private AssignmentItem? CreateAssignmentItem(Course c)
        {
            Console.WriteLine("Name:");
            var name = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Description:");
            var description = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Which assignment should be added?");
            c.Assignments.ToList().ForEach(Console.WriteLine);
            var choice = int.Parse(Console.ReadLine() ?? "-1");

            //bool nameExists = c.Assignments.Any(a => a.Name.Equals(choice, StringComparison.OrdinalIgnoreCase));

            if(choice >= 0)
            {
                var assignment = c.Assignments.FirstOrDefault(a => a.Id == choice);
                return new AssignmentItem
                {
                    Assignment = assignment,
                    Name = name,
                    Description = description
                };
               
            }
            return null;
        }
        private void SetUpModules(Course c)
        {
            Console.WriteLine("Do you want to add modules? (Y/N)");
            var userResponse = Console.ReadLine() ?? "N";

            bool contAdding;

            if(userResponse.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                contAdding = true;
                while(contAdding)
                {
                    c.Modules.Add(CreateModule(c));
                    Console.WriteLine("Add more modules? (Y/N)");
                    userResponse = Console.ReadLine() ?? "N";
                    if(userResponse.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                    {
                        contAdding = false;
                    }
                }
            }
           // AddModule();

        }
        //Add BQCK
      /*  public void AddOrUpdateCourse(CoursesDTO? courseSelected = null)
        {
            Console.WriteLine("Enter course code: ");
            var code = Console.ReadLine();

            Console.WriteLine("Enter course name: ");
            var name = Console.ReadLine();

            Console.WriteLine("Enter course description: ");
            var description = Console.ReadLine() ?? string.Empty;


            bool create = false;

            if (courseSelected == null)
            {
                create = true;
                courseSelected = new CoursesDTO();
            }

            courseSelected.Code = code;
            courseSelected.Name = name ?? string.Empty;
            courseSelected.Description = description;

            if (create)
            {
                courseService.Add(courseSelected);
            }
            courseService.Courses.ForEach(Console.WriteLine);*/
            /*
            Console.WriteLine("Do you want to add an assignment to this course? (yes/no)");
            var response = Console.ReadLine();
            if (response?.ToLower() == "yes")
            {
                var assignment = CreateAssignment();
                if (assignment != null)
                {
            
                    Console.WriteLine($"Assignment '{assignment.Name}' added to course {courseSelected.Name}.");
                }
            }*/

        //    SetUpAssignments(courseSelected);
        //   SetUpModules(courseSelected);
       
        //}

        //ADD BACL IF CAOUSES ERROR
      /*  public void AddModule()
        {
            Console.WriteLine("Enter the course code that the module will be added to:");
           courseService.Courses.ForEach(Console.WriteLine);
            var response = Console.ReadLine();
            var selctedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(response));
            if (selctedCourse != null)
            {
                selctedCourse.Modules.Add(CreateModule(selctedCourse));
            }
        }*/

        public void RemoveStudentFromCourse()
        {
            Console.WriteLine("Enter the course code to remove a student from:");
            var courseCode = Console.ReadLine();

            var course = courseService.Courses.FirstOrDefault(c => c.Code.Equals(courseCode, StringComparison.OrdinalIgnoreCase));
            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            if (course.Roster.Count == 0)
            {
                Console.WriteLine("There are no students enrolled in this course.");
                return;
            }

            Console.WriteLine("Enter student ID:");
            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                Console.WriteLine("Invalid student ID.");
                return;
            }

            var student = course.Roster.FirstOrDefault(s => s.Id == studentId);
            if (student == null)
            {
                Console.WriteLine("Student not found in this course.");
                return;
            }

            course.Roster.Remove(student);
            Console.WriteLine($"Student {student.Name} has been removed from the course {course.Name}.");
        }


        //public void AddStudentToCourse()
        //{

        //    Console.WriteLine("Enter the course code to add a student to:");
        //    var courseCode = Console.ReadLine();

        //    var course = courseService.Courses.FirstOrDefault(c => c.Code.Equals(courseCode, StringComparison.InvariantCultureIgnoreCase));
        //    if (course == null)
        //    {
        //        Console.WriteLine("Course not found.");
        //        return;
        //    }

        //    Console.WriteLine("Enter student ID:");
        //    if (!int.TryParse(Console.ReadLine(), out int studentId))
        //    {
        //        Console.WriteLine("Invalid student ID.");
        //        return;
        //    }

        //    var student = studentService.Students.FirstOrDefault(s => s.Id == studentId);  // Fetch student from StudentService
        //    if (student == null)
        //    {
        //        Console.WriteLine("Student not found.");
        //        return;
        //    }

        //    if (course.Roster.Any(s => s.Id == student.Id))
        //    {
        //        Console.WriteLine("Student already enrolled in this course.");
        //        return;
        //    }

        //    course.Roster.Add(student);
        //    Console.WriteLine($"Student {student.Name} added to course {course.Name}.");
        //}


        //ADD NACL IF ERROR


       /* public void UpdateCourseRecord()
        {
            courseService.Courses.ForEach(Console.WriteLine);
            Console.WriteLine("Enter code for course you want to update: ");
           

            var inputStr = Console.ReadLine();

           
                var courseSelected = courseService.Courses.FirstOrDefault(s => s.Code.Equals(inputStr, StringComparison.InvariantCultureIgnoreCase));
                if (courseSelected != null)
                {
                    AddOrUpdateCourse(courseSelected);
                }
            
        }*/

       /* public void ListCourses()
        {
            // courseService.Courses.ForEach(Console.WriteLine);
            courseService.Courses.ForEach(course => Console.WriteLine($"Code: {course.Code}, Name: {course.Name}"));

            Console.WriteLine("Enter the code of the course to view more details: ");
            var courseCode = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(c => c.Code.Equals(courseCode, StringComparison.OrdinalIgnoreCase));
            if (selectedCourse == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            if (selectedCourse.AssignmentGroups.Count == 0)
            {
                Console.WriteLine($"Code: {selectedCourse.Code}, Name: {selectedCourse.Name}");
                Console.WriteLine("There are no assignments for this course.");
                return;
            }

            //Console.WriteLine($"Assignments for {selectedCourse.Name}:");
            foreach (var assignment in selectedCourse.Assignments)
            {
                Console.WriteLine($"Code: {selectedCourse.Code}, Name: {selectedCourse.Name}, Description: {selectedCourse.Description}");
                Console.WriteLine($"Assignments for {selectedCourse.Name}:");
                Console.WriteLine($"Name: {assignment.Name}, Description: {assignment.Description}, Due Date: {assignment.DueDate}, Total Points: {assignment.TotalPointsAvailable}");
            }

            if (selectedCourse == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }
            
            // Displaying modules and their content
            if (selectedCourse.Modules != null && selectedCourse.Modules.Any())
            {
                Console.WriteLine("Modules:");
                foreach (var module in selectedCourse.Modules)
                {
                    Console.WriteLine($"- Module Name: {module.Name}, Description: {module.Description}");
                    // Ensure module.Content has elements before trying to access
                    if (module.Content.Any())
                    {
                        Console.WriteLine("  Content:");
                        foreach (var contentItem in module.Content)
                        {
                            if (contentItem is AssignmentItem assignmentItem)
                            {
                                // Display details specific to the assignment
                                Console.WriteLine($"    - Assignment Name: {assignmentItem.Assignment.Name}, Description: {assignmentItem.Assignment.Description}, Due Date: {assignmentItem.Assignment.DueDate}, Total Points: {assignmentItem.Assignment.TotalPointsAvailable}");
                            }
                            // Since we're now sure contentItem is from a List that should not contain nulls,
                            // directly print out its properties.
                            Console.WriteLine($"    - Content Name: {contentItem.Name}, Description: {contentItem.Description}");
                            // Add additional details here if necessary, like 'Path' if it's relevant.
                        }
                    }
                    else
                    {
                        Console.WriteLine("  No content available in this module.");
                    }
                }
            }
            else
            {
                Console.WriteLine("There are no modules for this course.");
            }
        }*/

        //Add BACK IF CAUSES ERROR
       /* public void SearchForCourses(string? query = null)
        {
            if(string.IsNullOrEmpty(query))
            {
                courseService.Courses.ForEach(Console.WriteLine);
            }
            else
            {
                {
                    courseService.Search(query).ToList().ForEach(Console.WriteLine);
                }
            }
            Console.WriteLine("Select a course:");

            var code = Console.ReadLine() ?? string.Empty;

            var courseSelected = courseService.Courses.FirstOrDefault(c => c.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase));
            if (courseSelected != null)
            {
                Console.WriteLine(courseSelected.Display);
            }*/
           
            /*Console.WriteLine("Enter a query: ");
            var query = Console.ReadLine() ?? string.Empty;

            // courseService.Search(query).ToList().ForEach(Console.WriteLine);
           var searchResult =  courseService.Search(query).ToList();

            if (!searchResult.Any())
            {
                Console.WriteLine("No courses found.");
                return;
            }

            Console.WriteLine("Search Results:");
            foreach (var course in searchResult)
            {
                Console.WriteLine($"Code: {course.Code}, Name: {course.Name}, Description: {course.Description}");

                if (course.Assignments.Any())
                {
                    Console.WriteLine("Assignments for this course:");
                    foreach (var assignment in course.Assignments)
                    {
                        Console.WriteLine($"  Name: {assignment.Name}, Description: {assignment.Description}, Due Date: {assignment.DueDate}, Total Points: {assignment.TotalPointsAvailable}");
                    }
                }
                else
                {
                    Console.WriteLine("  No assignments for this course.");
                }
            }*/


     //   }

        //ADD BACK IF CAOUSES ERROR
       /* public void ListSubmissions()
        {
            Console.WriteLine("Enter the course code you would like to add the assignment to:");
            courseService.Courses.ForEach(Console.WriteLine);
            var choice = Console.ReadLine();

            var courseSelected = courseService.Courses.FirstOrDefault(s => s.Code.Equals(choice, StringComparison.InvariantCultureIgnoreCase));
            if (courseSelected != null)
            {
                courseSelected.Submissions.ForEach(Console.WriteLine);  
            }
        }
       */


    }
}
