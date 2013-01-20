Conjurer
========

Conjurer is a clever .NET implementation of the [Test Data Builder](http://www.natpryce.com/articles/000714.html) pattern that helps you set up objects in the arrange part of your tests.  Although useful in many situations, Conjurer is particularly good at setting up POCO style entity objects used with Entity Framework, NHibernate and similar persistence frameworks.

##Getting started
Use the "Presto" incantation to define a factory and build an object:

    Presto.Define<Person>(p => p.Name = "Somebody");

    var somebody = Presto.Create<Person>();
    
Passing parameters to the constructor:

    Presto.Define<Person>(() => new Person("Somebody"));

Building custom objects:

    var houdini = Presto.Create<Person>(x => x.Name = "Houdini");

Setting up a named factory and creating an object:

    Presto.Define<Person>("magician", p => 
    {
        p.Name = "Somebody";
        p.Role = Role.Magician;
    });

    var magician = Presto.Create<Person>("magician");

Chaining factories to stay DRY:

    var personFactory = Presto.Define<Person>(p => p.Name = "Somebody");

    Presto.Define<Person>("magician", personFactory, x => x.Role = Role.Magician);
    
    // Create a magician named "Somebody"
    var magician = Presto.Create<Person>("magician");
    
##Sequences##
Sequences yield consective numbers and are useful when each object produced by a factory must have unique properties. 

Using the default sequence: 

    Presto.Define<Person>(p => 
    {
        int number = Presto.Sequence.Next();
        p.Name = string.Format("person{0}", number);
    });
    
    var person0 = Presto.Create();
    var person1 = Presto.Create();
    
Setting up a custom sequence:

    Presto.Sequence.Add<Person>(100);

    Presto.Define<Person>(p =>
    {
        int number = Presto.Sequence.Next<Person>();
        p.Name = string.Format("person{0}", number);
    });

    var person100 = Presto.Create<Person>();
    var person101 = Presto.Create<Person>();
    
To reset sequences between tests:

    Presto.Sequence.Reset();

##Collections and Recursion##
Factories can create complex obejct graphs:

    var personFactory = Presto.Define<Person>(p =>
    {
        int number = Presto.Sequence.Next();
        p.Name = string.Format("person{0}", number);
    });

    Presto.Define<Person>("magician", personFactory, x => x.Role = Role.Magician);
    Presto.Define<Person>("spectator", personFactory, x => x.Role = Role.Spectator);

    Presto.Define<Performance>(p =>
    {
        p.Magician = Presto.Create<Person>("magician");
        p.Assistant = Presto.Create<Person>();
        p.Audience = Presto.Create<Person>(10, "spectator");
    });
    
    // Create performance with 1 magician named Houdini, 1 assistant and 10 spectators
    var performance = Presto.Create<Performance>(p => p.Magician.Name = "Houdini");
    
##Persistence##
Since Conjurer makes no assumptions about your choice of persistence framework, you must set up the appropriate persistence logic for your project manually. 

If you are using Entity Framework, the following will do:

    MagicianContext db = new MagicianContext();

    Presto.PersistAction = entity =>
    {
        db.Set(entity.GetType()).Add(entity);
        db.SaveChanges();
    };
Note that the database object is defined outside the scope of the delegate to avoid costly initialization every time an object is persisted.

To create and persist an object in one go:

    Presto.Persist<Person>();
    
To create or persist an object, depending on the context:

    Presto.Define<Person>("magician", x => x.Role = Role.Magician);
    Presto.Define<Performance>(p => p.Magician = Presto.CreateOrPersist<Person>("magician"));

    // Create both performance and magician in memory only
    var created = Presto.Create<Performance>();
    
    // Persist both performance and magician
    var persisted = Presto.Persist<Performance>();
    
The `Presto.CreateOrPersist` method examines the call stack to see if it contains a call to `Presto.Persist`. If such a call is present higher up in the call stack, the created object will be persisted.    
