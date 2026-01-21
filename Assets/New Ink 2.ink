VAR doorResult = ""

=== door_confirm ===
業務に進みますか？

+ はい
    ~ doorResult = "YES"
    -> DONE

+ いいえ
    ~ doorResult = "NO"
    -> DONE
