// DEBUG SHORTCUTS - set false in release!
VAR DEBUG = false
{DEBUG:
	IN DEBUG MODE!
	who is calling?
	*	[Dad ...]	-> dad
	*	[Kevin ...] -> kevin 
	*	[hon ...] -> hon
- else:
	// Randomize route: where do we begin?
 {~->dad|->kevin|->hon}
}

// neutral option in round 2 always ends the route


=== dad ===
- You there? #Dad
Sorry if this is a bad time. #Dad
call me #Dad
    * [. . .] #Player
        Come on. I know you're there. Who doesn't keep their phone on them? You ALWAYS do this and it's getting old. #Dad
        we NEED to talk #Dad
    * (pos1) everything ok?[] what's going on? #Player
        Call me #Dad
        It's about Mom #Dad
    * (neg1) kinda busy[] #Player
        talk later, k? #Player
        Seriously? #Dad
- This can't really wait another day #Dad
You need to go see her. #Dad
    * I'm good #Player
        Look, I know you've had your differences #Dad
        You know her. She's old-fashioned. #Dad
        None of that matter anymore #Dad
        All that crap between you two isn't going to mean anything when she's gone, you know? #Dad
        -> DONE
    * (pos2) {pos1} How long[?] does she have? #Player
        A month? Weeks? You never know these things #Dad
        I know you've had your differences. #Dad
        But I think you both need this. You NEED to talk this out or you're gonna regret it. Can't you just call her? It'd mean a lot #Dad
    * (neg2) {neg1} Still busy[]. I'm fine. Really. #Player
        unbelievable. #Dad
        just unbelievable. #Dad
    * (posA) {not pos1} sorry #Player   // effective neutral for negative route
        it's not too late #Dad
        lets talk #Dad
    * (negA) {not neg1} [. . .] #Player // effective neutral for positive route
        Come on. #Dad
- ? #Dad
* {pos2} I'll try[] but I don't owe her anything. She says ANYTHING and I'm out #Player
        I get it #Dad
        thanks. #Dad
* {neg2} What about me?[] Look. I don't owe her anything. you think its for my benefit? you just want to let her go with a clean conscience #Player
* {(pos1 or posA) and (neg1 or negA)} I'll think about it[], ok? NO promises #Player
        thanks #Dad
* {posA or negA} bye
- -> DONE
    
    
=== kevin ===
- YO #Kevin
    * Um? #Player
        lol #Kevin
        hey man can you take my shift for Friday? #Kevin
        I have an appt #Kevin
        Its really important #Kevin
        come on  #Kevin
        I'll owe you #Kevin
    *(pos1) Hey, man[.] What's up? #Player
        Suppppppp #Kevin
        How would you like a Friday shift? #Kevin
        I got plans #Kevin
        if I call in again lol im gonna get axed #Kevin
    *(neg1) No[] in advance. Not again. #Player
        dont do me like that #Kevin
        I'll trade your Thursday for my Friday #Kevin
        and bring you coffee #Kevin
        plzzzz #Kevin
- Bruh #Kevin
    * Nope #Player
        boo #Kevin
        booooooo #Kevin
        boooooooooooo #Kevin
        fine :< #Kevin
        -> DONE
    * (pos2) {pos1} [I guess]Yea, sure. I guess I can. You DO owe me #Player
        YOU #Kevin
        ARE #Kevin
        THE #Kevin
        BEST #Kevin
        Thankyou thankyou thankyou #Kevin
        I'll send you the schedule #Kevin
        name ur price!! #Kevin
    * (neg2) {neg1} [rather not] idk... i'd rather not #Player
        its fine man #Kevin
        I wont twist your arm #Kevin
    * (posA) {not pos1} [i have plans] cant... got plans #Player
        yea i heard about ur mom #Kevin
        sorry to hear it #Kevin
    * (negA) {not neg1} [. . .] #Player
- sry! #Kevin
* {pos2} [No worries] lol no worries #Player
* {neg2} talk to you later #Player
        peace #Kevin
* {(pos1 or posA) and (neg1 or negA)} [. . .] #Player
        point taken #Kevin
        ttyl #Kevin
* {posA or negA} g2g #Player
- -> DONE


=== hon ===
- <3 :) #Hon
    * Hey #Player
        Hey :V #Hon
        Your dad called trying get a hold of u  #Hon
        call him back #Hon
        or dont. idc~  #Hon
    *(pos1) <3 uwu #Player
        such a dork lol #Hon
        XD hey ur dad called btw #Hon
        sounds important? Think its ur mom...? #Hon
    *(neg1) >_> #Player
        rude </3 #Hon
        oh and your dad called
- <3? #Hon
    * [Not now] I don't really wanna deal with it rn #Player
        I don't really think 'later' is an option anymore, babe #Hon
        but its your call #Hon
        -> DONE
    * (pos2) {pos1} [It's a mess] I don't even know what I would say. Its a mess. #Player
        Yea... I get that <3 #Hon
        Just do your best, ok? I'm right here whatever you decide #Hon
    * (neg2) {neg1} its not my problem #Player
        I dont think you really believe that #Hon
        calling them might do you some good #Hon
    * (posA) {not pos1} Can you handle it? #Player
        I can talk to your dad but... #Hon
        I think your answer is better coming from YOU <3 #Hon
    * (negA) {not neg1} I can't do this #Player
        no one's making you decide right away. take your time #Hon
- omw home btw #Hon
* {pos2} [Thanks] I could really use your help, thanks #Player
    np np I'm just glad we can figure this out together #Hon
    see you soon ok? #Hon
* {neg2} [Tomorrow] we can talk about it tomorrow #Player
    you said that yesterday...  #Hon
    we gotta figure this out soon, bae #Hon
    see you soon #Hon
* {(pos1 or posA) and (neg1 or negA)} [we'll talk later] we can talk when you're home I GUESS #Player
    so generous <3 #Hon
    and we had better! #Hon
* {posA or negA} bring coffee #Player
    on it!  #Hon
- -> DONE

