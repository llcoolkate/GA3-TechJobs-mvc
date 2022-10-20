using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TechJobsMVCAutograded.Models;

namespace TechJobsMVCAutograded.Data
{
    public class JobData
    {
        static private string DATA_FILE = "Data/job_data.csv";

        static bool IsDataLoaded;

        static List<Job> AllJobs;
        static private List<JobField> AllEmployers = new List<JobField>();
        static private List<JobField> AllLocations = new List<JobField>();
        static private List<JobField> AllPositionTypes = new List<JobField>();
        static private List<JobField> AllCoreCompetencies = new List<JobField>();

        static public List<Job> FindAll()
        {
            LoadData();

            return new List<Job>(AllJobs);
        }

        static public List<Job> FindByColumnAndValue(string column, string value)
        {

            // load data, if not already loaded
            LoadData();

            List<Job> jobs = new List<Job>();

            if (value.ToLower().Equals("all"))
            {
                return FindAll();
            }

            if (column.Equals("all"))
            {
                jobs = FindByValue(value);
                return jobs;
            }

            for (int i = 0; i < AllJobs.Count; i++)
            {
                Job job = AllJobs[i];
                string aValue = GetFieldValue(job, column);

                if (aValue != null && aValue.ToLower().Contains(value.ToLower()))
                {
                    jobs.Add(job);
                }
            }

            return jobs;
        }

        static public string GetFieldValue(Job job, string fieldName)
        {
            string theValue;
            if (fieldName.Equals("name"))
            {
                theValue = job.Name;
            }
            else if (fieldName.Equals("employer"))
            {
                theValue = job.Employer.ToString();
            }
            else if (fieldName.Equals("location"))
            {
                theValue = job.Location.ToString();
            }
            else if (fieldName.Equals("positionType"))
            {
                theValue = job.PositionType.ToString();
            }
            else
            {
                theValue = job.CoreCompetency.ToString();
            }

            return theValue;
        }

        static public List<Job> FindByValue(string value)
        {

            // load data, if not already loaded
            LoadData();

            List<Job> jobs = new List<Job>();

            for (int i = 0; i < AllJobs.Count; i++)
            {

                if (AllJobs[i].Name.ToLower().Contains(value.ToLower()))
                {
                    jobs.Add(AllJobs[i]);
                }
                else if (AllJobs[i].Employer.ToString().ToLower().Contains(value.ToLower()))
                {
                    jobs.Add(AllJobs[i]);
                }
                else if (AllJobs[i].Location.ToString().ToLower().Contains(value.ToLower()))
                {
                    jobs.Add(AllJobs[i]);
                }
                else if (AllJobs[i].PositionType.ToString().ToLower().Contains(value.ToLower()))
                {
                    jobs.Add(AllJobs[i]);
                }
                else if (AllJobs[i].CoreCompetency.ToString().ToLower().Contains(value.ToLower()))
                {
                    jobs.Add(AllJobs[i]);
                }

            }

            return jobs;
        }

        static private object FindExistingObject(List<JobField> fieldlist, string value)
        {
            for (int i = 0; i < fieldlist.Count; i++)
            {
                object item = fieldlist[i];
                if (item.ToString().ToLower().Equals(value.ToLower()))
                {
                    return item;
                }
            }
            return null;
        }

        static private void LoadData()
        {
            if (AllJobs == null || AllJobs.Count == 0)
            {
                IsDataLoaded = false;
            }

            if (IsDataLoaded)
            {
                return;
            }

            List<string[]> rows = new List<string[]>();


            using (StreamReader reader = File.OpenText(DATA_FILE))
            {
                while (reader.Peek() >= 0)
                {
                    string line = reader.ReadLine();
                    string[] rowArray = CSVRowToStringArray(line);
                    if (rowArray.Length > 0)
                    {
                        rows.Add(rowArray);
                    }
                }
            }

            string[] headers = rows[0];
            rows.Remove(headers);

            AllJobs = new List<Job>();

            // Parse each row array 
            for (int i = 0; i < rows.Count; i++ ) // (string[] row in rows)
            {
                string[] row = rows[i];
                string aName = row[0];
                string anEmployer = row[1];
                string aLocation = row[2];
                string aPositionType = row[3];
                string aCoreCompetency = row[4];

                Employer newEmployer = (Employer)FindExistingObject(AllEmployers, anEmployer);
                Location newLocation = (Location)FindExistingObject(AllLocations, aLocation);
                PositionType newPositionType = (PositionType)FindExistingObject(AllPositionTypes, aPositionType);
                CoreCompetency newCoreCompetency = (CoreCompetency)FindExistingObject(AllCoreCompetencies, aCoreCompetency);

                if (newEmployer == null)
                {
                    newEmployer = new Employer(anEmployer);
                    AllEmployers.Add(newEmployer);
                }

                if (newLocation == null)
                {
                    newLocation = new Location(aLocation);
                    AllLocations.Add(newLocation);
                }

                if (newPositionType == null)
                {
                    newPositionType = new PositionType(aPositionType);
                    AllPositionTypes.Add(newPositionType);
                }

                if (newCoreCompetency == null)
                {
                    newCoreCompetency = new CoreCompetency(aCoreCompetency);
                    AllCoreCompetencies.Add(newCoreCompetency);
                }

                Job newJob = new Job(aName, newEmployer, newLocation, newPositionType, newCoreCompetency);

                AllJobs.Add(newJob);
            }

            IsDataLoaded = true;
        }

        private static string[] CSVRowToStringArray(string row, char fieldSeparator = ',', char stringSeparator = '\"')
        {
            bool isBetweenQuotes = false;
            StringBuilder valueBuilder = new StringBuilder();
            List<string> rowValues = new List<string>();

            // Loop through the row string one char at a time
            for (int i = 0; i < row.ToCharArray().Length; i ++) // (char c in row.ToCharArray())
            {
                char c = row.ToCharArray()[i];

                if ((c == fieldSeparator && !isBetweenQuotes))
                {
                    rowValues.Add(valueBuilder.ToString());
                    valueBuilder.Clear();
                }
                else
                {
                    if (c == stringSeparator)
                    {
                        isBetweenQuotes = !isBetweenQuotes;
                    }
                    else
                    {
                        valueBuilder.Append(c);
                    }
                }
            }

            // Add the final value
            rowValues.Add(valueBuilder.ToString());
            valueBuilder.Clear();

            return rowValues.ToArray();
        }

        static public List<JobField> GetAllEmployers()
        {
            LoadData();
            AllEmployers.Sort(new NameSorter());
            return AllEmployers;
        }

        static public List<JobField> GetAllLocations()
        {
            LoadData();
            AllLocations.Sort(new NameSorter());
            return AllLocations;
        }

        static public List<JobField> GetAllPositionTypes()
        {
            LoadData();
            AllPositionTypes.Sort(new NameSorter());
            return AllPositionTypes;
        }

        static public List<JobField> GetAllCoreCompetencies()
        {
            LoadData();
            AllCoreCompetencies.Sort(new NameSorter());
            return AllCoreCompetencies;
        }
    }

}
