VAR resultScore = 0

=== EndStart ===
-> End1
-> CheckResult

=== CheckResult ===
{ resultScore <= 0:
    -> End5
- resultScore == 1:
    -> End4
- resultScore == 2:
    -> End2
- else:
    -> End3
}

=== End 1 ===
新しいバイトの子だよね？
お疲れ様～！
-> DONE
=== End 2 ===
仕事の出来はまぁまぁかな。
うん、初日だしいいんだけど、
つぎはくれぐれもがんばってね。
-> END
=== End 3 ===
仕事よくできてたみたいだね！
初日なのによく頑張ったじゃん？
あ、次のシフトなんだけどさぁ
 
辞める？いや、まだ初日だし
え？まってまって！？
-> END
=== End 4 ===
ところで
苦情すごいんだけど、何？
次から来なくていいよ。
-> END
=== End 5 ===
ふざけやがって！！
今日の給料絶対に出さねぇからな！！！
二度と面見せんなよ！！！！
-> END