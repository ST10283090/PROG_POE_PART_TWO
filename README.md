# PROG7312_POE FINAL— Municipal ity App

## HOW TO COMPILE, RUN, AND USE THE PROGRAMME:

It is a Visual Studios MVC app and ut lets users report municipality issues (potholes, broken lights, water leaks). It stores each report in memory and shows them in different ways so you can see how different data structures affect retrieval, sorting, and prioritisation.

---

### WHat you will need to compile and run the app:

Install .NET SDK 8.0+ and Visual Studios 2022.

Download this repo as a zip.

Unzip it in your preferred location.

Open Visual Studios 2022 -> File -> Open -> and Loacte your file in the same place that u unzipped it.

---

### From the project folder:

```bash
cd PROG7312_POE
dotnet build
dotnet run
OR

Click on the green run button to test the app.
```

## How to use the app

### Report an Issue
Users can report an issue by clicking on report issues in the nav bar or report issues button on the home page, they will then fill in the details, optionally upload a photo/doc. The app assigns a Report ID and saves the issue locally, automatically.

---

### Service Requests Status
The user can view their reported issues as well as the status of aid issue by going to the Service Requests Status page using the nav bar, here they can enter a Report ID to find a single report, or 0 to list all.

---

### Local Events and Announcements
Users can see a list of all local events and announcements added to the app. They can sort through said list, by date or event category to fiind what best suits them. THere is also a recommendation feature that recommends events based on the users search hiatory.

---

## IN-DEPTH EXPLANATIN

The app made use of four data structures, each of which affected and displayed the reported issues in a different way:

- Street Number ASC (BST)  
- Category A → Z (AVL)  
- Address Length ASC (Heap)  
- Latest Entries First (Graph)  

---

## THE DIFFERENT DATA STRUctures

---

## Data structures, what they do, and why they help

### 1. Custom linked list — the baseline
**What it is:** a list that keeps reports in submission order.  
**When it helps:** when you want a chronological list of everything submitted.  
**Real-world example:** an admin opens the system and wants to see the most recent reports as they came in to check for duplication. The linked list showed the reports in that order. Simpleand straightfoward.

---

### 2. Binary Search Tree (BST) — street-number / address ordering
**What it is:** a tree that places items to the left or right by comparing street address strings.

**Why sort by street/address?**  
Streets and house numbers are what dispatchers use when sending out workers. If a technician calls and says “we need to check 717 Andrew Zondo Road,” a structure ordered by address lets you jump to nearby addresses faster, or list results in address order so similar addresses will appear together.

Sorting by address groups houses together, which makes manual inspection faster. Say there are many issues in one area, a view of the reports by address can help to easily see duplicates or send out workers for one area easily.

---

### 3. AVL Tree — category A → Z fast and predictable
**What it is:** a self-balancing binary tree that keeps height minimal while inserting/removing. The app inserts by ReportId, then you run an in-order traversal and display by category alphabetically.

**Why alphabetical categories matter:**  
Say the issues have been assigned to grouos of workers by category, so gor example, the team in charge of "Water" complaints wants to see all water-related reports A→Z. Alphabetical categories lets them scan categories quickly without needing to go over the whole dataset.

---

### 4. MinHeap — quick queries set my address lenght in my case
**What it is:** a binary heap stored as a list. The heap property guarantees the element with the smallest key is going to be shown first. I used reoprtID when viewing the reports initially, but when viewing with heap the list and sort by StreetAddress.Length to show shortest addresses first.

**When can thi be useful:**  
If the municipality wants to send out a task tem to smaller streets with fewer houses, say during the day, they can filter by street lenght as that will have less traffic so less people are disturbed and workers are also safer as less cars will be on the roadway.

---

### 5. Graph — latest-first reporting
It’s a simple undirected graph using an adjacency list. Each report is a node, and each new report connects to the one before it. The graph shows how reports are linked by order or related events.

**Why a graph helps:**  
Say the municipalitysorts by latest reports first using the grpah then sees many new complaints all about the same issue, then they know the issue is real and needs attenindg to, so they can send out a team ASAP.
