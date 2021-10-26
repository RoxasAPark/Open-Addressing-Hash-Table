using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAddressingHashTable
{
    class Program
    {
        // Class to define an employee object to insert into the hash table
        class Employee
        {
            private string firstName;
            private string lastName;

            // Each employee is assigned an integer as ID prior to insertion to the table
            private int ID;

            // Constructor for the employee object
            public Employee(string fName, string lName, int idNum)
            {
                this.firstName = fName;
                this.lastName = lName;
                this.ID = idNum;
            }

            public string getFirstName()
            {
                return this.firstName;
            }

            public string getLastName()
            {
                return this.lastName;
            }

            public int getID()
            {
                return this.ID;
            }
        }

        // Class for the hash table equipped with open addressing functions
        class HashTable
        {
            // Number of indices for the table
            private int indices;
            private Employee[] table;

            // An integer to keep count of Employee objects inserted into the table
            private int count;

            // Constructor to initialize the hash table
            public HashTable(int i)
            {
                indices = i;
                table = new Employee[indices];
                count = 0;
            }

            // A function to check if there are no more empty indices left
            public bool isFull()
            {
                return (count == table.Length);
            }

            // Hash function to assign an employee an index
            public int hashFunction(int key)
            {
                return (key % indices);
            }

            // Function to insert an employee to the hash table
            public void insert(string first, string last, int k)
            {
                // Get the index to assign an employee
                int index = hashFunction(k);

                if (isFull())
                    Console.WriteLine("The table is full.");
                else
                {
                    // Insert into the index if empty
                    if (table[index] == null)
                    {
                        table[index] = new Employee(first, last, k);
                    }
                    else // Find the next empty index
                    {
                        int currentIndex = index;
                        while (table[currentIndex] != null)
                        {
                            if (currentIndex == table.Length - 1)
                                currentIndex = 0;

                            currentIndex++;
                        }

                        // Insert into the empty index if found
                        table[currentIndex] = new Employee(first, last, k);
                    }

                    // Update the count
                    count++;
                }            
            }

            // Function to remove an employee from the table
            public void delete(int k)
            {
                // An index cannot be negative
                if (k < 0)
                    return;
                else
                {
                    // Locate the index where the employee may be
                    int index = hashFunction(k);

                    // If the employee is found, remove it immediately
                    if (table[index].getID() == k)
                        table[index] = null;
                    else
                    {
                        // Otherwise, search every other index until the employee is found
                        bool keyFound = false;
                        int current;

                        for(current = 0; current < table.Length;current++)
                        {
                            if(table[current].getID() == k)
                            {
                                keyFound = true;
                                break;
                            }
                        }

                        if (keyFound == true)
                            table[index] = null;
                        else
                            Console.WriteLine("An employee with that ID number doesn't exist");

                    }
                }
            }

            // Function to output the hash table
            public void outputTable()
            {
                for(int i = 0; i < table.Length; i++)
                {
                    if (table[i] != null)
                        Console.WriteLine(i + ": " + table[i].getFirstName() + " " + table[i].getLastName()
                            + " " + table[i].getID());
                }
            }
        }

        static void Main(string[] args)
        {
            HashTable ht = new HashTable(6);

            ht.insert("Priscilla", "Betti", 19);
            ht.insert("Hikaru", "Nakamura", 16);
            ht.insert("Lorie", "Pester", 18);
            ht.insert("Magnus", "Carlsen", 11);
            ht.insert("Falco", "Lombardi", 41);
            ht.insert("Tifa", "Lockhart", 48);

            ht.insert("Eddie", "Pulaski", 10);

            ht.delete(11);

            ht.outputTable();
        }
    }
}
