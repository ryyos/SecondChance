public class DialogueData
{
    public Opening opening { get; set; }
    public Negotiation negotiation { get; set; }
    public Accept accept { get; set; }
    public Reject reject { get; set; }
    public Misc misc { get; set; }
}

// opening
public class Opening
{
    public OpeningCustomer customer { get; set; }
    public OpeningSeller seller { get; set; }
}


public class OpeningCustomer
{
    public string[] greeting { get; set; }
    public string[] context { get; set; }
    public string[] follow_up { get; set; }
}

public class OpeningSeller
{
    public string[] reaction { get; set; }
    public string[] follow_up { get; set; }
    public string[] emotion { get; set; }
}

// negotiation
public class Negotiation
{
    public NegotiationCustomer customer { get; set; }
    public NegotiationSeller seller { get; set; }
}

public class NegotiationCustomer
{
    public string[] reaction { get; set; }
    public string[] nego { get; set; }
}

public class NegotiationSeller
{
    public string[] nego { get; set; }
    public string[] emotion { get; set; }
}

// accept
public class Accept
{
    public AcceptCustomer customer { get; set; }
    public AcceptSeller seller { get; set; }
}

public class AcceptCustomer
{
    public string[] accept { get; set; }
}

public class AcceptSeller
{
    public string[] accept { get; set; }
}

// reject
public class Reject
{
    public RejectCustomer customer { get; set; }
    public RejectSeller seller { get; set; }
}

public class RejectCustomer
{
    public string[] reject { get; set; }
}

public class RejectSeller
{
    public string[] reject { get; set; }
}

// misc
public class Misc
{
    public string[] goodbye { get; set; }
    public string[] remark { get; set; }
}