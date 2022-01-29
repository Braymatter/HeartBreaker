{~->dad|->kevin}

// neutral option in round 2 always ends the route

=== dad ===
- (round1) Dad: You there?
Dad: Sorry if this is a bad time.
Dad: call me
    * [. . .]
        Dad: Come on. I know you're there. Who doesn't keep their phone on them? You ALWAYS do this and it's getting old.
        Dad: we NEED to talk
    * (pos1) You: everything ok?[] what's going on?
        Dad: Call me
        Dad: It's about Mom
    * (neg1) You: kinda busy[] 
        You: talk later, k?
        Dad: Seriously?
- (round2) Dad: This can't really wait another day
Dad: You need to go see her.
    * You: I'm good
        Dad: Look, I know you've had your differences
        Dad: You know her. She's old-fashioned. 
        Dad: None of that matter anymore
        Dad: All that crap between you two isn't going to mean anything when she's gone, you know? 
        -> DONE
    * (pos2) {pos1} You: How long[?] does she have?
        Dad: A month? Weeks? You never know these things
        Dad: I know you've had your differences.
        Dad: But I think you both need this. You NEED to talk this out or you're gonna regret it. Can't you just call her? It'd mean a lot  
        // round 3 pos
    * (neg2) {neg1} You: Still busy[]. I'm fine. Really.
        Dad: unbelievable.
        Dad: just unbelievable.
        // round 3 neg
    * (posA) {not pos1} You: sorry        // effective neutral for negative route
        Dad: it's not too late
        Dad: lets talk
    * (negA) {not neg1} [. . .]        // effective neutral for positive route
        Dad: Come on.
- (round3) 
* {pos2} I'll try[] but I don't owe her anything. She says ANYTHING and I'm out
        Dad: I get it
        Dad: thanks.
* {neg2} What about me?[] Look. I don't owe her anything. you think its for my benefit? you just want to let her go with a clean conscience
* {(pos1 or posA) and (neg1 or negA)} I'll think about it[], ok? NO promises
        Dad: thanks
- -> DONE
    
=== kevin ===
- (round1) Kevin: YO
    * You: Um?
        Kevin: lol
        Kevin: hey man can you take my shift for Friday? 
        Kevin: I have an appt
        Kevin: Its really important
        Kevin: come on 
        Kevin: I'll owe you
    *(pos1) You: Hey, man[.] What's up?
        Kevin: Suppppppp
        Kevin: How would you like a Friday shift?
        Kevin: I got plans
        Kevin: if I call in again lol im gonna get axed
    *(neg1) You: No[] in advance. Not again.
        Kevin: dont do me like that
        Kevin: I'll trade your Thursday for my Friday
        Kevin: and bring you coffee
        Kevin: plzzzz
- (round2) Kevin: Bruh
    * Neutral Route
        -> DONE
    * (pos2) {pos1} positive 2
    * (neg2) {neg1} negative 2
    * (posA) {not pos1} positive a
    * (negA) {not neg1} negative a
- (round3) 
* {pos2} positive 3
* {neg2} negative 3
* {(pos1 or posA) and (neg1 or negA)} neutral
- -> DONE
        
    
- -> DONE

