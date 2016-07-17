﻿/**
* Copyright 2015 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/


using UnityEngine;
using System.Collections;
using IBM.Watson.DeveloperCloud.Services.PersonalityInsights.v2;
using IBM.Watson.DeveloperCloud.Logging;

namespace IBM.Watson.DeveloperCloud.UnitTests
{
    public class TestPersonalityInsights : UnitTest {
        PersonalityInsights m_personalityInsights = new PersonalityInsights();
        private string testString = "Facing certain defeat at the hands of a room-size I.B.M. computer on Wednesday evening, Ken Jennings, famous for winning 74 games in a row on the TV quiz show, acknowledged the obvious. \"I, for one, welcome our new computer overlords,\" he wrote on his video screen, borrowing a line from a \"Simpsons\" episode.\n\nFrom now on, if the answer is \"the computer champion on \"Jeopardy!,\" the question will be, \"What is Watson?\"\n\nFor I.B.M., the showdown was not merely a well-publicized stunt and a $1 million prize, but proof that the company has taken a big step toward a world in which intelligent machines will understand and respond to humans, and perhaps inevitably, replace some of them.\n\nWatson, specifically, is a \"question answering machine\" of a type that artificial intelligence researchers have struggled with for decades — a computer akin to the one on \"Star Trek\" that can understand questions posed in natural language and answer them.\n\nWatson showed itself to be imperfect, but researchers at I.B.M. and other companies are already developing uses for Watson's technologies that could have a significant impact on the way doctors practice and consumers buy products.\n\n\"Cast your mind back 20 years and who would have thought this was possible?\" said Edward Feigenbaum, a Stanford University computer scientist and a pioneer in the field.\n\nIn its \"Jeopardy!\" project, I.B.M. researchers were tackling a game that requires not only encyclopedic recall, but also the ability to untangle convoluted and often opaque statements, a modicum of luck, and quick, strategic button pressing.\n\nThe contest, which was taped in January here at the company's T. J. Watson Research Laboratory before an audience of I.B.M. executives and company clients, played out in three televised episodes concluding Wednesday. At the end of the first day, Watson was in a tie with Brad Rutter, another ace human player, at $5,000 each, with Mr. Jennings trailing with $2,000.\n\nBut on the second day, Watson went on a tear. By night's end, Watson had a commanding lead with a total of $35,734, compared with Mr. Rutter's $10,400 and Mr. Jennings's $4,800.\n\nVictory was not cemented until late in the third match, when Watson was in Nonfiction. \"Same category for $1,200,\" it said in a manufactured tenor, and lucked into a Daily Double. Mr. Jennings grimaced.\n\nEven later in the match, however, had Mr. Jennings won another key Daily Double it might have come down to Final Jeopardy, I.B.M. researchers acknowledged.\n\nThe final tally was $77,147 to Mr. Jennings's $24,000 and Mr. Rutter's $21,600.\n\nMore than anything, the contest was a vindication for the academic field of artificial intelligence, which began with great promise in the 1960s with the vision of creating a thinking machine and which became the laughingstock of Silicon Valley in the 1980s, when a series of heavily financed start-up companies went bankrupt.\n\nDespite its intellectual prowess, Watson was by no means omniscient. On Tuesday evening during Final Jeopardy, the category was U.S. Cities and the clue was: \"Its largest airport is named for a World War II hero; its second largest for a World War II battle.\"\n\nWatson drew guffaws from many in the television audience when it responded \"What is Toronto?????\"\n\nThe string of question marks indicated that the system had very low confidence in its response, I.B.M. researchers said, but because it was Final Jeopardy, it was forced to give a response. The machine did not suffer much damage. It had wagered just $947 on its result. (The correct answer is, \"What is Chicago?\")\n\n\"We failed to deeply understand what was going on there,\" said David Ferrucci, an I.B.M. researcher who led the development of Watson. \"The reality is that there's lots of data where the title is U.S. cities and the answers are countries, European cities, people, mayors. Even though it says U.S. cities, we had very little confidence that that's the distinguishing feature.\"\n\nThe researchers also acknowledged that the machine had benefited from the \"buzzer factor.\"\n\nBoth Mr. Jennings and Mr. Rutter are accomplished at anticipating the light that signals it is possible to \"buzz in,\" and can sometimes get in with virtually zero lag time. The danger is to buzz too early, in which case the contestant is penalized and \"locked out\" for roughly a quarter of a second.\n\nWatson, on the other hand, does not anticipate the light, but has a weighted scheme that allows it, when it is highly confident, to hit the buzzer in as little as 10 milliseconds, making it very hard for humans to beat. When it was less confident, it took longer to  buzz in. In the second round, Watson beat the others to the buzzer in 24 out of 30 Double Jeopardy questions.\n\n\"It sort of wants to get beaten when it doesn't have high confidence,\" Dr. Ferrucci said. \"It doesn't want to look stupid.\"\n\nBoth human players said that Watson's button pushing skill was not necessarily an unfair advantage. \"I beat Watson a couple of times,\" Mr. Rutter said.\n\nWhen Watson did buzz in, it made the most of it. Showing the ability to parse language, it responded to, \"A recent best seller by Muriel Barbery is called 'This of the Hedgehog,' \" with \"What is Elegance?\"\n\nIt showed its facility with medical diagnosis. With the answer: \"You just need a nap. You don't have this sleep disorder that can make sufferers nod off while standing up,\" Watson replied, \"What is narcolepsy?\"\n\nThe coup de grâce came with the answer, \"William Wilkenson's 'An Account of the Principalities of Wallachia and Moldavia' inspired this author's most famous novel.\" Mr. Jennings wrote, correctly, Bram Stoker, but realized that he could not catch up with Watson's winnings and wrote out his surrender.\n\nBoth players took the contest and its outcome philosophically.\n\n\"I had a great time and I would do it again in a heartbeat,\" said Mr. Jennings. \"It's not about the results; this is about being part of the future.\"\n\nFor I.B.M., the future will happen very quickly, company executives said. On Thursday it plans to announce that it will collaborate with Columbia University and the University of Maryland to create a physician's assistant service that will allow doctors to query a cybernetic assistant. The company also plans to work with Nuance Communications Inc. to add voice recognition to the physician's assistant, possibly making the service available in as little as 18 months.\n\n\"I have been in medical education for 40 years and we're still a very memory-based curriculum,\" said Dr. Herbert Chase, a professor of clinical medicine at Columbia University who is working with I.B.M. on the physician's assistant. \"The power of Watson- like tools will cause us to reconsider what it is we want students to do.\"\n\nI.B.M. executives also said they are in discussions with a major consumer electronics retailer to develop a version of Watson, named after I.B.M.'s founder, Thomas J. Watson, that would be able to interact with consumers on a variety of subjects like buying decisions and technical support.\n\nDr. Ferrucci sees none of the fears that have been expressed by theorists and science fiction writers about the potential of computers to usurp humans.\n\n\"People ask me if this is HAL,\" he said, referring to the computer in \"2001: A Space Odyssey.\" \"HAL's not the focus; the focus is on the computer on 'Star Trek,' where you have this intelligent information seek dialogue, where you can ask follow-up questions and the computer can look at all the evidence and tries to ask follow-up questions. That's very cool.\"";


        bool m_GetProfileTextTested = false;
        bool m_GetProfileJsonTested = false;

        public override IEnumerator RunTest()
        {
            string dataPath = Application.dataPath + "/Watson/Examples/ServiceExamples/TestData/personalityInsights.json";

            if ( Utilities.Config.Instance.FindCredentials( m_personalityInsights.GetServiceID() ) == null )
                yield break;

            Log.Debug("TestPersonalityInsights", "Attempting GetProfile using Text!");
            m_personalityInsights.GetProfile(OnGetProfileText, testString);
            while(!m_GetProfileTextTested)
                yield return null;

            Log.Debug("TestPersonalityInsights", "Attempting GetProfile using Json!");
            m_personalityInsights.GetProfile(OnGetProfileJson, dataPath);
            while(!m_GetProfileJsonTested)
                yield return null;

            yield break;
        }

        private void OnGetProfileText(Profile profile, string data)
        {
            Test(profile != null);

            if(profile != null)
            {
                if(!string.IsNullOrEmpty(profile.id))
                    Log.Debug("ExamplePersonalityInsights", "id: {0}", profile.id);
                if(!string.IsNullOrEmpty(profile.source))
                    Log.Debug("ExamplePersonalityInsights", "source: {0}", profile.source);
                if(!string.IsNullOrEmpty(profile.processed_lang))
                    Log.Debug("ExamplePersonalityInsights", "proccessed_lang: {0}", profile.processed_lang);
                if(!string.IsNullOrEmpty(profile.word_count))
                    Log.Debug("ExamplePersonalityInsights", "word_count: {0}", profile.word_count);
                if(!string.IsNullOrEmpty(profile.word_count_message))
                    Log.Debug("ExamplePersonalityInsights", "word_count_message: {0}", profile.word_count_message);

                if(profile.tree != null)
                {
                    LogTraitTree(profile.tree);
                }

                m_GetProfileTextTested = true;
            }
        }

        private void OnGetProfileJson(Profile profile, string data)
        {
            Test(profile != null);

            if(profile != null)
            {
                if(!string.IsNullOrEmpty(profile.id))
                    Log.Debug("ExamplePersonalityInsights", "id: {0}", profile.id);
                if(!string.IsNullOrEmpty(profile.source))
                    Log.Debug("ExamplePersonalityInsights", "source: {0}", profile.source);
                if(!string.IsNullOrEmpty(profile.processed_lang))
                    Log.Debug("ExamplePersonalityInsights", "proccessed_lang: {0}", profile.processed_lang);
                if(!string.IsNullOrEmpty(profile.word_count))
                    Log.Debug("ExamplePersonalityInsights", "word_count: {0}", profile.word_count);
                if(!string.IsNullOrEmpty(profile.word_count_message))
                    Log.Debug("ExamplePersonalityInsights", "word_count_message: {0}", profile.word_count_message);

                if(profile.tree != null)
                {
                    LogTraitTree(profile.tree);
                }

                m_GetProfileJsonTested = true;
            }
        }

        private void LogTraitTree(TraitTreeNode traitTreeNode)
        {
            if(!string.IsNullOrEmpty(traitTreeNode.id))
                Log.Debug("ExamplePersonalityInsights", "id: {0}", traitTreeNode.id);
            if(!string.IsNullOrEmpty(traitTreeNode.name))
                Log.Debug("ExamplePersonalityInsights", "name: {0}", traitTreeNode.name);
            if(!string.IsNullOrEmpty(traitTreeNode.category))
                Log.Debug("ExamplePersonalityInsights", "category: {0}", traitTreeNode.category);
            if(!string.IsNullOrEmpty(traitTreeNode.percentage))
                Log.Debug("ExamplePersonalityInsights", "percentage: {0}", traitTreeNode.percentage);
            if(!string.IsNullOrEmpty(traitTreeNode.sampling_error))
                Log.Debug("ExamplePersonalityInsights", "sampling_error: {0}", traitTreeNode.sampling_error);
            if(!string.IsNullOrEmpty(traitTreeNode.raw_score))
                Log.Debug("ExamplePersonalityInsights", "raw_score: {0}", traitTreeNode.raw_score);
            if(!string.IsNullOrEmpty(traitTreeNode.raw_sampling_error))
                Log.Debug("ExamplePersonalityInsights", "raw_sampling_error: {0}", traitTreeNode.raw_sampling_error);
            if(traitTreeNode.children != null && traitTreeNode.children.Length > 0)
                foreach(TraitTreeNode childNode in traitTreeNode.children)
                    LogTraitTree(childNode);
        }
    }
}
