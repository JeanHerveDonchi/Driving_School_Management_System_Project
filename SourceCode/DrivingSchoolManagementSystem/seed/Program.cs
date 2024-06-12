namespace seed
{
    internal class Program
    {
        

        public class Lesson
        {
            public int InstructorId { get; set; }
            public int StudentId { get; set; }
            public int CarId { get; set; }
            public DateTime LessonDate { get; set; }
            public TimeSpan LessonStartTime { get; set; }
            public TimeSpan LessonEndTime { get; set; }
            public TimeSpan Duration => LessonEndTime - LessonStartTime;
        }

        static void Main(string[] args)
        {
            void SeedLessons()
            {
                var random = new Random();
                var instructors = new List<int> { /* List of instructor IDs */ };
                var students = new List<int> { /* List of student IDs */ };
                var cars = new List<int> { /* List of car IDs */ };

                var startDate = new DateTime(2019, 1, 1);
                var endDate = new DateTime(2025, 12, 31);
                var lessonDurationOptions = new List<TimeSpan>
                {
                    TimeSpan.FromMinutes(30),
                    TimeSpan.FromMinutes(60),
                    TimeSpan.FromMinutes(90),
                    TimeSpan.FromMinutes(120),
                    TimeSpan.FromMinutes(150),
                    TimeSpan.FromMinutes(180)
                };

                var lessons = new List<Lesson>();

                for (int i = 0; i < 40; i++)
                {
                    var lesson = new Lesson();
                    lesson.InstructorId = instructors[random.Next(instructors.Count)];
                    lesson.StudentId = students[random.Next(students.Count)];
                    lesson.CarId = cars[random.Next(cars.Count)];

                    bool validLesson = false;
                    while (!validLesson)
                    {
                        var lessonDate = startDate.AddDays(random.Next((endDate - startDate).Days));
                        var lessonStartTime = TimeSpan.FromHours(random.Next(8, 20)); // Lessons between 8 AM and 8 PM
                        var lessonDuration = lessonDurationOptions[random.Next(lessonDurationOptions.Count)];
                        var lessonEndTime = lessonStartTime.Add(lessonDuration);

                        // Check for overlapping lessons for the same instructor, car, and student on the same day
                        if (!lessons.Any(l => l.InstructorId == lesson.InstructorId && l.LessonDate == lessonDate &&
                                               ((l.LessonStartTime <= lessonStartTime && l.LessonEndTime > lessonStartTime) ||
                                                (l.LessonStartTime < lessonEndTime && l.LessonEndTime >= lessonEndTime)) ||
                                               (l.CarId == lesson.CarId && l.LessonDate == lessonDate &&
                                               ((l.LessonStartTime <= lessonStartTime && l.LessonEndTime > lessonStartTime) ||
                                                (l.LessonStartTime < lessonEndTime && l.LessonEndTime >= lessonEndTime)) ||
                                               (l.StudentId == lesson.StudentId && l.LessonDate == lessonDate))))
                        {
                            lesson.LessonDate = lessonDate;
                            lesson.LessonStartTime = lessonStartTime;
                            lesson.LessonEndTime = lessonEndTime;
                            validLesson = true;
                        }
                    }

                    lessons.Add(lesson);
                }

                foreach (var lesson in lessons)
                {
                    Console.WriteLine($"INSERT INTO Lessons (InstructorId, StudentId, CarId, LessonDate, LessonStartTime, LessonEndTime, Fee) " +
                                      $"VALUES ({lesson.InstructorId}, {lesson.StudentId}, {lesson.CarId}, '{lesson.LessonDate:yyyy-MM-dd}', " +
                                      $"'{lesson.LessonStartTime}', '{lesson.LessonEndTime}', {lesson.Duration.TotalHours * 60});");
                }
            }

            SeedLessons();
        }
    }
}
